// ---------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="Corale">
//     Copyright © 2015-2022 by Adam Hellberg and Brandon Scott.
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy of
//     this software and associated documentation files (the "Software"), to deal in
//     the Software without restriction, including without limitation the rights to
//     use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//     of the Software, and to permit persons to whom the Software is furnished to do
//     so, subject to the following conditions:
//
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//     CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------

namespace Colore.Native.Kernel32
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Native methods from <c>kernel32</c> module.
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Name of the DLL from which functions are imported.
        /// </summary>
        //// For some reason FxCop doesn't see that this field is used.
#pragma warning disable CA1823 // Avoid unused private fields
        private const string DllName = "kernel32.dll";
#pragma warning restore CA1823 // Avoid unused private fields

        /// <summary>
        /// Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        /// </summary>
        /// <param name="module">
        /// A handle to the DLL module that contains the function or variable.
        /// The <c>LoadLibrary</c>, <c>LoadLibraryEx</c>, <c>LoadPackagedLibrary</c>, or <c>GetModuleHandle</c> function returns this handle.
        /// The <c>GetProcAddress</c> function does not retrieve addresses from modules that were loaded using the <c>LOAD_LIBRARY_AS_DATAFILE</c> flag.
        /// For more information, see <c>LoadLibraryEx</c>.
        /// </param>
        /// <param name="procName">
        /// The function or variable name, or the function's ordinal value. If this parameter is an ordinal value, it must be in the low-order word; the high-order word must be zero.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the address of the exported function or variable.
        /// If the function fails, the return value is <c>NULL</c>.To get extended error information, call <c>GetLastError</c>.
        /// </returns>
        /// <remarks>
        /// The spelling and case of a function name pointed to by <c>lpProcName</c> must be identical to that in the
        /// <c>EXPORTS</c> statement of the source DLL's module-definition (<c>.def</c>) file. The exported names of
        /// functions may differ from the names you use when calling these functions in your code. This difference is
        /// hidden by macros used in the SDK header files. For more information, see Conventions for Function Prototypes.
        /// <para>
        /// The <c>lpProcName</c> parameter can identify the DLL function by specifying an ordinal value associated with
        /// the function in the <c>EXPORTS</c> statement. <c>GetProcAddress</c> verifies that the specified ordinal is in
        /// the range 1 through the highest ordinal value exported in the <c>.def</c> file. The function then uses the ordinal
        /// as an index to read the function's address from a function table.
        /// </para>
        /// <para>
        /// If the <c>.def</c> file does not number the functions consecutively from <c>1</c> to <c>N</c> (where <c>N</c> is
        /// the number of exported functions), an error can occur where <c>GetProcAddress</c> returns an invalid,
        /// non-<c>NULL</c> address, even though there is no function with the specified ordinal.
        /// </para>
        /// <para>
        /// If the function might not exist in the DLL module - for example, if the function is available only on
        /// Windows Vista but the application might be running on Windows XP - specify the function by name rather
        /// than by ordinal value and design your application to handle the case when the function is not available.
        /// </para>
        /// </remarks>
        #pragma warning disable SA1118 // The parameter spans multiple lines
        [SuppressMessage(
            "Globalization",
            "CA2101:Specify marshaling for P/Invoke string arguments",
            Justification =
                "GetProcAddress does not have a unicode equivalent. BestFitMapping is disabled and ThrowOnUnmappableChar is enabled to make sure there is no security issue.")]
        #pragma warning restore SA1118 // The parameter spans multiple lines
        [DllImport(
            DllName,
            CharSet = CharSet.Ansi,
            EntryPoint = nameof(GetProcAddress),
            ExactSpelling = true,
            SetLastError = true,
            BestFitMapping = false,
            ThrowOnUnmappableChar = true)]
        internal static extern IntPtr GetProcAddress(IntPtr module, string procName);

        /// <summary>
        /// Loads the specified module into the address space of the calling process. The specified module may cause other modules to be loaded.
        /// </summary>
        /// <param name="filename">
        /// The name of the module. This can be either a library module (a <c>.dll</c> file) or an executable module (an <c>.exe</c> file).
        /// The name specified is the file name of the module and is not related to the name stored in the library module itself,
        /// as specified by the <c>LIBRARY</c> keyword in the module-definition (<c>.def</c>) file.
        /// <para>
        /// If the string specifies a full path, the function searches only that path for the module.
        /// </para>
        /// <para>
        /// If the string specifies a relative path or a module name without a path, the function uses a standard search strategy
        /// to find the module; for more information, see the Remarks.
        /// </para>
        /// <para>
        /// If the function cannot find the module, the function fails. When specifying a path, be sure to use backslashes (<c>\</c>),
        /// not forward slashes (<c>/</c>). For more information about paths, see Naming a File or Directory.
        /// </para>
        /// <para>
        /// If the string specifies a module name without a path and the file name extension is omitted, the function appends the
        /// default library extension <c>.dll</c> to the module name. To prevent the function from appending <c>.dll</c> to the module name,
        /// include a trailing point character (<c>.</c>) in the module name string.
        /// </para>
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the module.
        /// <para>If the function fails, the return value is <c>NULL</c>.To get extended error information, call <c>GetLastError</c>.</para>
        /// </returns>
        /// <remarks>
        /// To enable or disable error messages displayed by the loader during DLL loads, use the <c>SetErrorMode</c> function.
        /// <para>
        /// <c>LoadLibrary</c> can be used to load a library module into the address space of the process and return a handle
        /// that can be used in <c>GetProcAddress</c> to get the address of a DLL function. <c>LoadLibrary</c> can also be used
        /// to load other executable modules. For example, the function can specify an .exe file to get a handle that can be used
        /// in <c>FindResource</c> or <c>LoadResource</c>. However, do not use <c>LoadLibrary</c> to run an .exe file. Instead,
        /// use the <c>CreateProcess</c> function.
        /// </para>
        /// <para>
        /// If the specified module is a DLL that is not already loaded for the calling process, the system calls the DLL's
        /// <c>DllMain</c> function with the <c>DLL_PROCESS_ATTACH</c> value. If <c>DllMain</c> returns <c>TRUE</c>,
        /// <c>LoadLibrary</c> returns a handle to the module. If <c>DllMain</c> returns <c>FALSE</c>, the system unloads
        /// the DLL from the process address space and <c>LoadLibrary</c> returns <c>NULL</c>. It is not safe to call
        /// <c>LoadLibrary</c> from <c>DllMain</c>. For more information, see the Remarks section in <c>DllMain</c>.
        /// </para>
        /// <para>
        /// Module handles are not global or inheritable. A call to <c>LoadLibrary</c> by one process does not produce a handle
        /// that another process can use — for example, in calling <c>GetProcAddress</c>. The other process must make its own call
        /// to <c>LoadLibrary</c> for the module before calling <c>GetProcAddress</c>.
        /// </para>
        /// <para>
        /// If lp<c>FileName</c> does not include a path and there is more than one loaded module with the same base name and
        /// extension, the function returns a handle to the module that was loaded first.
        /// </para>
        /// <para>
        /// If no file name extension is specified in the lp<c>FileName</c> parameter, the default library extension .dll is
        /// appended. However, the file name string can include a trailing point character (.) to indicate that the module name
        /// has no extension. When no path is specified, the function searches for loaded modules whose base name matches the
        /// base name of the module to be loaded. If the name matches, the load succeeds. Otherwise, the function searches
        /// for the file.
        /// </para>
        /// <para>
        /// The first directory searched is the directory containing the image file used to create the calling process
        /// (for more information, see the <c>CreateProcess</c> function). Doing this allows private dynamic-link library (DLL)
        /// files associated with a process to be found without adding the process's installed directory to the <c>PATH</c>
        /// environment variable. If a relative path is specified, the entire relative path is appended to every token in the
        /// DLL search path list. To load a module from a relative path without searching any other path, use <c>GetFullPathName</c>
        /// to get a nonrelative path and call <c>LoadLibrary</c> with the nonrelative path. For more information on the DLL search
        /// order, see Dynamic-Link Library Search Order.
        /// </para>
        /// <para>
        /// The search path can be altered using the <c>SetDllDirectory</c> function. This solution is recommended instead of using
        /// <c>SetCurrentDirectory</c> or hard-coding the full path to the DLL.
        /// </para>
        /// <para>
        /// If a path is specified and there is a redirection file for the application, the function searches for the module in the
        /// application's directory. If the module exists in the application's directory, <c>LoadLibrary</c> ignores the specified
        /// path and loads the module from the application's directory. If the module does not exist in the application's directory,
        /// <c>LoadLibrary</c> loads the module from the specified directory. For more information,
        /// see Dynamic Link Library Redirection.
        /// </para>
        /// <para>
        /// If you call <c>LoadLibrary</c> with the name of an assembly without a path specification and the assembly is listed
        /// in the system compatible manifest, the call is automatically redirected to the side-by-side assembly.
        /// </para>
        /// <para>
        /// The system maintains a per-process reference count on all loaded modules. Calling <c>LoadLibrary</c> increments the
        /// reference count. Calling the <c>FreeLibrary</c> or <c>FreeLibraryAndExitThread</c> function decrements the reference
        /// count. The system unloads a module when its reference count reaches zero or when the process terminates (regardless
        /// of the reference count).
        /// </para>
        /// <para>
        /// Windows Server 2003 and Windows XP:  The Visual C++ compiler supports a syntax that enables you to declare thread-local
        /// variables: <c>_declspec(thread)</c>. If you use this syntax in a DLL, you will not be able to load the DLL explicitly
        /// using <c>LoadLibrary</c> on versions of Windows prior to Windows Vista. If your DLL will be loaded explicitly,
        /// you must use the thread local storage functions instead of <c>_declspec(thread)</c>. For an example, see Using
        /// Thread Local Storage in a Dynamic Link Library.
        /// </para>
        /// <para>
        /// Do not use the <c>SearchPath</c> function to retrieve a path to a DLL for a subsequent <c>LoadLibrary</c> call.
        /// The <c>SearchPath</c> function uses a different search order than <c>LoadLibrary</c> and it does not use safe process
        /// search mode unless this is explicitly enabled by calling <c>SetSearchPathMode</c> with
        /// <c>BASE_SEARCH_PATH_ENABLE_SAFE_SEARCHMODE</c>. Therefore, <c>SearchPath</c> is likely to first search the user’s
        /// current working directory for the specified DLL. If an attacker has copied a malicious version of a DLL into the
        /// current working directory, the path retrieved by <c>SearchPath</c> will point to the malicious DLL, which
        /// <c>LoadLibrary</c> will then load.
        /// </para>
        /// <para>
        /// Do not make assumptions about the operating system version based on a <c>LoadLibrary</c> call that searches for a DLL.
        /// If the application is running in an environment where the DLL is legitimately not present but a malicious version of
        /// the DLL is in the search path, the malicious version of the DLL may be loaded. Instead, use the recommended techniques
        /// described in Getting the System Version.
        /// </para>
        /// </remarks>
        [DllImport(
            DllName,
            CharSet = CharSet.Ansi,
            EntryPoint = nameof(LoadLibrary),
            SetLastError = true,
            BestFitMapping = false,
            ThrowOnUnmappableChar = true)]
        internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string filename);

        /// <summary>
        /// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        /// When the reference count reaches zero, the module is unloaded from the address space of the calling
        /// process and the handle is no longer valid.
        /// </summary>
        /// <param name="hModule">
        /// A handle to the loaded library module. The <see cref="LoadLibrary" />, <c>LoadLibraryEx</c>,
        /// <c>GetModuleHandle</c>, or <c>GetModuleHandleEx</c> function returns this handle.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is <c>true</c>.
        /// <para>
        /// If the function fails, the return value is <c>false</c>.
        /// To get extended error information, call the <c>GetLastError</c> function.
        /// </para>
        /// </returns>
        /// <remarks>
        /// <para>
        /// The system maintains a per-process reference count for each loaded module.
        /// A module that was loaded at process initialization due to load-time dynamic linking
        /// has a reference count of one. The reference count for a module is incremented each time the module
        /// is loaded by a call to <see cref="LoadLibrary" />. The reference count is also incremented
        /// by a call to <c>LoadLibraryEx</c> unless the module is being loaded for the first time and
        /// is being loaded as a data or image file.
        /// </para>
        /// <para>
        /// The reference count is decremented each time the <see cref="FreeLibrary" /> or
        /// <c>FreeLibraryAndExitThread</c> function is called for the module.
        /// When a module's reference count reaches zero or the process terminates, the system unloads the module
        /// from the address space of the process.
        /// Before unloading a library module, the system enables the module to detach from the process
        /// by calling the module's <c>DllMain</c> function, if it has one, with the <c>DLL_PROCESS_DETACH</c> value.
        /// Doing so gives the library module an opportunity to clean up resources allocated on behalf of
        /// the current process.
        /// After the entry-point function returns, the library module is removed from the address space of
        /// the current process.
        /// </para>
        /// <para>
        /// It is not safe to call <see cref="FreeLibrary" /> from <c>DllMain</c>.
        /// For more information, see the Remarks section in <c>DllMain</c>.
        /// </para>
        /// <para>
        /// Calling <see cref="FreeLibrary" /> does not affect other processes that are using the same module.
        /// </para>
        /// <para>
        /// Use caution when calling <see cref="FreeLibrary" /> with a handle returned by <c>GetModuleHandle</c>.
        /// The <c>GetModuleHandle</c> function does not increment a module's reference count,
        /// so passing this handle to <see cref="FreeLibrary" /> can cause a module to be unloaded prematurely.
        /// </para>
        /// <para>
        /// A thread that must unload the DLL in which it is executing and then terminate itself
        /// should call <c>FreeLibraryAndExitThread</c> instead of calling <see cref="FreeLibrary" />
        /// and <c>ExitThread</c> separately.
        /// Otherwise, a race condition can occur.
        /// For details, see the Remarks section of <c>FreeLibraryAndExitThread</c>.
        /// </para>
        /// </remarks>
        [DllImport(DllName, EntryPoint = nameof(FreeLibrary), ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(IntPtr hModule);
    }
}
