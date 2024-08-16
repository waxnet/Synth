using System.Runtime.InteropServices;

namespace Synth
{
    public static class Files
    {
        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool GetOpenFileName(ref OpenFileName ofn);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct OpenFileName
        {
            public int lStructSize;
            public IntPtr hwndOwner;
            public IntPtr hInstance;
            public string lpstrFilter;
            public string lpstrCustomFilter;
            public int nMaxCustFilter;
            public int nFilterIndex;
            public string lpstrFile;
            public int nMaxFile;
            public string lpstrFileTitle;
            public int nMaxFileTitle;
            public string lpstrInitialDir;
            public string lpstrTitle;
            public int Flags;
            public short nFileOffset;
            public short nFileExtension;
            public string lpstrDefExt;
            public IntPtr lCustData;
            public IntPtr lpfnHook;
            public string lpTemplateName;
            public IntPtr pvReserved;
            public int dwReserved;
            public int flagsEx;
        }

        // methods
        public static string AskForScript()
        {
            OpenFileName openFileName = new();
            openFileName.lStructSize = Marshal.SizeOf(openFileName);
            openFileName.lpstrFilter = "Lua Files (*.lua)\0*.lua\0";
            openFileName.lpstrFile = new string(new char[256]);
            openFileName.nMaxFile = openFileName.lpstrFile.Length;
            openFileName.lpstrFileTitle = new string(new char[64]);
            openFileName.nMaxFileTitle = openFileName.lpstrFileTitle.Length;
            openFileName.lpstrTitle = "Select Script";

            if (GetOpenFileName(ref openFileName))
                return openFileName.lpstrFile;
            return "";
        }
    }
}
