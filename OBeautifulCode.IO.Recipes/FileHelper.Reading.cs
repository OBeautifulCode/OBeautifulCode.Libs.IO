﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileHelper.Reading.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// <auto-generated>
//   Sourced from NuGet package. Will be overwritten with package update except in OBeautifulCode.IO.Recipes source.
// </auto-generated>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.IO.Recipes
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    using static System.FormattableString;

#if !OBeautifulCodeIORecipesProject
    internal
#else
    public
#endif
    static partial class FileHelper
    {
        /// <summary>
        /// Counts the number of lines in a file.
        /// </summary>
        /// <remarks>
        /// A line is considered one of the following:
        /// - some or no text followed by a newline
        /// - some text that does not end in a newline
        /// A zero byte file is considered 0 lines.
        /// A file with just a newline is considered 1 line (1 instance of no text followed by a newline).
        /// A file with some text and a newline is considered 1 line (1 instance of text followed by a newline).
        /// </remarks>
        /// <param name="filePath">file to count lines.</param>
        /// <returns>number of lines in the file.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath"/> is empty, not in legal form, has illegal characters in path, or points to a Win32 device.</exception>
        /// <exception cref="FileNotFoundException">file wasn't found on disk.</exception>
        /// <exception cref="DirectoryNotFoundException"><paramref name="filePath"/> path is invalid.</exception>
        /// <exception cref="PathTooLongException"><paramref name="filePath"/> was too long.</exception>
        /// <exception cref="NotSupportedException">The <paramref name="filePath"/>'s format is not supported.</exception>
        /// <exception cref="IOException">There's an IO error accessing file.</exception>
        public static long CountLines(
            string filePath)
        {
            long count = 0;
            using (var reader = new StreamReader(filePath))
            {
                while (reader.ReadLine() != null)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Counts the number of non-blank lines in a file.
        /// </summary>
        /// <param name="filePath">file to count lines.</param>
        /// <returns>number of non-blank lines in the file.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath"/> is empty, not in legal form, has illegal characters in path, or points to a Win32 device.</exception>
        /// <exception cref="FileNotFoundException">file wasn't found on disk.</exception>
        /// <exception cref="DirectoryNotFoundException"><paramref name="filePath"/> path is invalid.</exception>
        /// <exception cref="PathTooLongException"><paramref name="filePath"/> was too long.</exception>
        /// <exception cref="NotSupportedException">The <paramref name="filePath"/>'s format is not supported.</exception>
        /// <exception cref="IOException">There's an IO error accessing file.</exception>
        public static long CountNonblankLines(
            string filePath)
        {
            long count = 0;

            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        count++;
                    }
                } // while not end of file
            }

            return count;
        }

        /// <summary>
        /// Gets the MD5 hash of a file.
        /// </summary>
        /// <param name="filePath">file to calculate MD5.</param>
        /// <returns>the MD5 hash.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath"/> is whitespace, contains invalid characters, or refers to a non-file device such as "con:", "com1:", etc.</exception>
        /// <exception cref="FileNotFoundException">File specified by <paramref name="filePath"/> cannot be found.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="DirectoryNotFoundException">The directory containing file specified by filePath could not be found or the filePath is a directory.</exception>
        /// <exception cref="UnauthorizedAccessException">Caller doesn't have read permissions on file.</exception>
        /// <exception cref="PathTooLongException"><paramref name="filePath"/> was too long.</exception>
        /// <exception cref="NotSupportedException"><paramref name="filePath"/> is in an invalid format.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Md", Justification = "This is spelled correctly.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Md", Justification = "This is cased as we would like it.")]
        public static string Md5(
            string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(Invariant($"'{nameof(filePath)}' is white space"));
            }

            byte[] hash;
            using (var md5Provider = new MD5CryptoServiceProvider())
            {
                using (Stream reader = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hash = md5Provider.ComputeHash(reader);
                }
            }

            return ToHexString(hash);
        }

        /// <summary>
        /// Converts an array of bytes to a uppercase hexidecimal string.
        /// </summary>
        /// <param name="value">byte array to convert.</param>
        /// <returns>lowercase hex string representing the input byte array.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public static string ToHexString(
            IList<byte> value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var output = new StringBuilder(value.Count * 2);
            foreach (byte t in value)
            {
                output.Append(t.ToString("X2", CultureInfo.CurrentCulture));
            }

            return output.ToString().ToUpper(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Reads all non-blank lines in a file to a list of string.
        /// </summary>
        /// <param name="filePath">file to read.</param>
        /// <returns>List of string containing all non-blank lines, or an empty List if there are none.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath"/> is whitespace, contains invalid characters, or refers to a non-file device such as "con:", "com1:", etc.</exception>
        /// <exception cref="PathTooLongException"><paramref name="filePath"/> has too many characters.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive), filePath is actually a directory.</exception>
        /// <exception cref="FileNotFoundException">The file specified in <paramref name="filePath"/> was not found.</exception>
        /// <exception cref="IOException">An I/O error occurred while opening the file, such as when the file is locked.</exception>
        /// <exception cref="UnauthorizedAccessException">Caller doesn't have the required permissions.</exception>
        /// <exception cref="NotSupportedException"><paramref name="filePath"/> is in an invalid format.</exception>
        /// <exception cref="SecurityException">The caller doesn't have the required permissions.</exception>
        public static ReadOnlyCollection<string> ReadAllNonblankLines(
            string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(Invariant($"'{nameof(filePath)}' is white space"));
            }

            string[] lines = File.ReadAllLines(filePath);
            var nonblankLines = lines.Where(line => !string.IsNullOrEmpty(line)).ToList();
            return new ReadOnlyCollection<string>(nonblankLines);
        }

        /// <summary>
        /// Returns the first line in a file after the header line.
        /// </summary>
        /// <param name="filePath">File to read.</param>
        /// <returns>String containing first non-header line in file.  If file has only a header line then empty string is returned.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath"/> is whitespace, contains invalid characters, or refers to a non-file device such as "con:", "com1:", etc.</exception>
        /// <exception cref="FileNotFoundException">File specified by <paramref name="filePath"/> cannot be found.</exception>
        /// <exception cref="InvalidOperationException">There is no header line.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="DirectoryNotFoundException">The directory containing file specified by filePath could not be found or the filePath is a directory.</exception>
        /// <exception cref="UnauthorizedAccessException">Caller doesn't have read permissions on file.</exception>
        /// <exception cref="PathTooLongException"><paramref name="filePath"/> was too long.</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="NotSupportedException"><paramref name="filePath"/> is in an invalid format.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "It's redundant but not harmful.")]
        public static string ReadFirstNonHeaderLine(
            string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(Invariant($"'{nameof(filePath)}' is white space"));
            }

            using (var filestream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new StreamReader(filestream))
                {
                    if (reader.EndOfStream)
                    {
                        throw new InvalidOperationException("There is no header line.");
                    }

                    reader.ReadLine();
                    if (reader.EndOfStream)
                    {
                        return string.Empty;
                    }

                    return reader.ReadLine();
                }
            }
        }

        /// <summary>
        /// Returns the first line in a file.
        /// </summary>
        /// <param name="filePath">File to read.</param>
        /// <returns>String containing first line in file.  If file has no lines, empty string is returned.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath"/> is whitespace, contains invalid characters, or refers to a non-file device such as "con:", "com1:", etc.</exception>
        /// <exception cref="FileNotFoundException">File specified by <paramref name="filePath"/> cannot be found.</exception>
        /// <exception cref="IOException">An I/O error occurs.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="DirectoryNotFoundException">The directory containing file specified by filePath could not be found or the filePath is a directory.</exception>
        /// <exception cref="UnauthorizedAccessException">Caller doesn't have read permissions on file.</exception>
        /// <exception cref="PathTooLongException"><paramref name="filePath"/> was too long.</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="NotSupportedException"><paramref name="filePath"/> is in an invalid format.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "We It's redundant but not harmful.")]
        public static string ReadHeaderLine(
            string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(Invariant($"'{nameof(filePath)}' is white space"));
            }

            using (var filestream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var reader = new StreamReader(filestream))
                {
                    if (reader.EndOfStream)
                    {
                        return string.Empty;
                    }

                    return reader.ReadLine();
                }
            }
        }

        /// <summary>
        /// Returns the last line in a file.
        /// </summary>
        /// <param name="filePath">file to read.</param>
        /// <returns>String containing last line of file.  If file has no lines then an empty string is returned.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath"/> is whitespace, contains invalid characters, or refers to a non-file device such as "con:", "com1:", etc.</exception>
        /// <exception cref="FileNotFoundException">File specified by <paramref name="filePath"/> cannot be found.</exception>
        /// <exception cref="IOException">An I/O error occurs, such as when the file is locked.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="DirectoryNotFoundException">The directory containing file specified by filePath could not be found or the filePath is a directory.</exception>
        /// <exception cref="UnauthorizedAccessException">Caller doesn't have read permissions on file.</exception>
        /// <exception cref="PathTooLongException"><paramref name="filePath"/> was too long.</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="NotSupportedException"><paramref name="filePath"/> is in an invalid format.</exception>
        public static string ReadLastLine(
            string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(Invariant($"'{nameof(filePath)}' is white space"));
            }

            using (var wholeStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                string text = null;
                long position = wholeStream.Length;

                while (position >= 0)
                {
                    wholeStream.Seek(position, SeekOrigin.Begin);

                    // Can't close nor dispose partialStream, it will cause fs to dispose prematurely
                    var partialStream = new StreamReader(wholeStream);

                    text = partialStream.ReadToEnd();
                    int endOfLineIndex = text.IndexOf(Environment.NewLine, StringComparison.CurrentCulture);
                    if (endOfLineIndex >= 0)
                    {
                        return text.Substring(endOfLineIndex + Environment.NewLine.Length);
                    }

                    position--;
                }

                // we get here if file has no lines or one line
                return text;
            }
        }

        /// <summary>
        /// Returns the last line in a file that's not blank.
        /// </summary>
        /// <param name="filePath">file to read.</param>
        /// <returns>String containing last line of file that's not blank.  If file has no lines then an empty string is returned.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath"/> is whitespace, contains invalid characters, or refers to a non-file device such as "con:", "com1:", etc.</exception>
        /// <exception cref="FileNotFoundException">File specified by <paramref name="filePath"/> cannot be found.</exception>
        /// <exception cref="IOException">An I/O error occurs, such as when the file is locked.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="DirectoryNotFoundException">The directory containing file specified by filePath could not be found or the filePath is a directory.</exception>
        /// <exception cref="UnauthorizedAccessException">Caller doesn't have read permissions on file.</exception>
        /// <exception cref="PathTooLongException"><paramref name="filePath"/> was too long.</exception>
        /// <exception cref="OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
        /// <exception cref="NotSupportedException"><paramref name="filePath"/> is in an invalid format.</exception>
        /// <exception cref="InvalidOperationException"><paramref name="filePath"/> points to a zero-byte file.</exception>
        public static string ReadLastNonblankLine(
            string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(Invariant($"'{nameof(filePath)}' is white space"));
            }

            using (var wholeStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                string text = null;
                long position = wholeStream.Length;
                int newLinesFound = 0;
                while (position >= 0)
                {
                    wholeStream.Seek(position, SeekOrigin.Begin);

                    // Can't close nor dispose partialStream, it will cause fs to dispose prematurely
                    var partialStream = new StreamReader(wholeStream);

                    text = partialStream.ReadToEnd();
                    int endOfLineIndex = text.IndexOf(Environment.NewLine, StringComparison.CurrentCulture);
                    if (endOfLineIndex >= 0)
                    {
                        string lineFound = text.Substring(endOfLineIndex + Environment.NewLine.Length);
                        var regex = new Regex(Environment.NewLine);
                        lineFound = regex.Replace(lineFound, string.Empty, newLinesFound);

                        if (string.IsNullOrEmpty(lineFound))
                        {
                            newLinesFound++;
                        }
                        else
                        {
                            return lineFound.Replace(Environment.NewLine, string.Empty);
                        }
                    }

                    position--;
                }

                // we get here if file has no lines or one line
                if (text == null)
                {
                    return string.Empty;
                }

                return text.Replace(Environment.NewLine, string.Empty);
            } // using wholeStream
        }

        /// <summary>
        /// Determines if a file is zero-bytes in size.
        /// </summary>
        /// <param name="filePath">path to file to evaluate.</param>
        /// <returns>True if file is zero-byte, false if not.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filePath"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="filePath"/> is whitespace or contains one or more invalid characters.</exception>
        /// <exception cref="SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">Access to <paramref name="filePath"/> is denied.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters.</exception>
        /// <exception cref="NotSupportedException"><paramref name="filePath"/> contains a colon (:) in the middle of the string.</exception>
        /// <exception cref="FileNotFoundException">The <paramref name="filePath"/> doesn't exist or the filePath is a directory.</exception>
        /// <exception cref="IOException">Couldn't get state of file.</exception>
        public static bool IsFileSizeZero(
            string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException(Invariant($"'{nameof(filePath)}' is white space"));
            }

            var info = new FileInfo(filePath);
            return info.Length == 0;
        }
    }
}