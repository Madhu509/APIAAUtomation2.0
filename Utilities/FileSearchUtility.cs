using System;
using System.IO;

public class FileSearchUtility
{
    public static string SearchFile(string fileName, string startFolder)
    {
        try
        {
            string[] files = Directory.GetFiles(startFolder, fileName, SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                // File found, return the first match (you can modify this to return all matches)
                return files[0];
            }
            else
            {
                // File not found in the specified folder and its subfolders
                throw new FileNotFoundException("File not found: ", fileName);
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur during the search
            return ex.Message;
        }
    }
}
