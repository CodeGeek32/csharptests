using System;
using System.Runtime.CompilerServices;

namespace APITests.Util
{
    internal static class ProjectSourcePath
    {
        private const string myRelativePath = nameof(ProjectSourcePath) + ".cs";
        private static string lazyValue;
        public static string Value => lazyValue = CalculatePath();

        private static string CalculatePath()
        {
            string pathName = GetSourceFilePathName();
            if (!pathName.EndsWith(myRelativePath, StringComparison.Ordinal))
                throw new Exception("Something wrong with path");
            return pathName.Substring(0, pathName.Length - myRelativePath.Length);
        }

        public static string GetSourceFilePathName([CallerFilePath] string callerFilePath = null) //
            => callerFilePath ?? "";
    }
}
