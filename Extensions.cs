﻿using System.Collections.Generic;
using System.IO;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security;
using Ookii.Dialogs.Wpf;

namespace OsuModeManager {
    public static class Extensions {
        public static bool TryGetFirst<T>(this IEnumerable<T> Enum, out T Result, bool Enumerate = false) {
            // ReSharper disable PossibleMultipleEnumeration
            if (Enumerate) { Enum = Enum.ToArray(); }
            if (Enum != null && Enum.Any()) {
                Result = Enum.First();
                return Result != null;
            }
            Result = default;
            return false;
            // ReSharper restore PossibleMultipleEnumeration
        }

        public static bool TryGetAt<T>(this T[] Array, int Index, out T Result) {
            if (Array == null || Index < 0 || Array.Length <= Index) {
                Result = default;
                return false;
            }
            Result = Array[Index];
            return Result != null;
        }
        public static bool IsNullOrEmpty(this string String) => string.IsNullOrEmpty(String);

        public static bool TryGetFile(this string FileName, out FileInfo File) {
#pragma warning disable CA1031 // Do not catch general exception types
            try {
                File = new FileInfo(FileName);
                return true;
            } catch (ArgumentNullException) { //FileName is null.
                Debug.WriteLine("FileName is null.", "ArgumentNullException");

            } catch (SecurityException) { //The caller does not have the required permission.
                Debug.WriteLine("The caller does not have the required permission.", "SecurityException");

            } catch (ArgumentException) { //The file name is empty, contains only white spaces, or contains invalid characters.
                Debug.WriteLine("The file name is empty, contains only white spaces, or contains invalid characters.", "ArgumentException");

            } catch (UnauthorizedAccessException) { //Access to FileName is denied.
                Debug.WriteLine("Access to FileName is denied.", "UnauthorizedAccessException");

            } catch (PathTooLongException) { //The specified path, file name, or both exceed the system-defined maximum length.
                Debug.WriteLine("The specified path, file name, or both exceed the system-defined maximum length.", "PathTooLongException");
            } catch (NotSupportedException) { //FileName contains a colon (:) in the middle of the string.
                Debug.WriteLine("FileName contains a colon (:) in the middle of the string.", "NotSupportedException");
            }

            File = null;
            return false;
#pragma warning restore CA1031 // Do not catch general exception types
        }
        public static bool TryGetDirectory(this string DirectoryName, out DirectoryInfo Directory) {
#pragma warning disable CA1031 // Do not catch general exception types
            try {
                Directory = new DirectoryInfo(DirectoryName);
                return true;
            } catch (ArgumentNullException) { //DirectoryName is null.
                Debug.WriteLine("DirectoryName is null.", "ArgumentNullException");

            } catch (SecurityException) { //The caller does not have the required permission.
                Debug.WriteLine("The caller does not have the required permission.", "SecurityException");

            } catch (ArgumentException) { //The Directory name is empty, contains only white spaces, or contains invalid characters.
                Debug.WriteLine("The Directory name is empty, contains only white spaces, or contains invalid characters.", "ArgumentException");

            } catch (UnauthorizedAccessException) { //Access to DirectoryName is denied.
                Debug.WriteLine("Access to DirectoryName is denied.", "UnauthorizedAccessException");

            } catch (PathTooLongException) { //The specified path, Directory name, or both exceed the system-defined maximum length.
                Debug.WriteLine("The specified path, Directory name, or both exceed the system-defined maximum length.", "PathTooLongException");
            } catch (NotSupportedException) { //DirectoryName contains a colon (:) in the middle of the string.
                Debug.WriteLine("DirectoryName contains a colon (:) in the middle of the string.", "NotSupportedException");
            }

            Directory = null;
            return false;
#pragma warning restore CA1031 // Do not catch general exception types
        }

        public static DirectoryInfo GetDirectory(Environment.SpecialFolder StartFolder = Environment.SpecialFolder.LocalApplicationData, string Description = "", bool UseDescriptionForTitles = true, bool ShowNewFolderButton = true, string SelectedPath = default) {
            VistaFolderBrowserDialog FolderBrowser = new VistaFolderBrowserDialog {
                ShowNewFolderButton = ShowNewFolderButton,
                RootFolder = StartFolder,
                UseDescriptionForTitle = UseDescriptionForTitles,
                Description = Description
            };

            if (!SelectedPath.IsNullOrEmpty()) {
                FolderBrowser.SelectedPath = SelectedPath;
            }

            if (FolderBrowser.ShowDialog() == true && TryGetDirectory(FolderBrowser.SelectedPath, out DirectoryInfo Result)) {
                return Result;
            }

            return null;
        }

        public static int Clamp(this int Value, int Min = int.MinValue, int Max = int.MaxValue) => Value < Min ? Min : Value > Max ? Max : Value;

    }
}