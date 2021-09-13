using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ParrelSync
{
public static class Clones
{
	/// <summary>
	/// Name used for an identifying file created in the clone project directory.
	/// </summary>
	/// <remarks>
	/// (!) Do not change this after the clone was created, because then connection will be lost.
	/// </remarks>
	public const string CloneFileName = ".clone";

        /// <summary>
        /// Name of the file for storing clone's argument.
        /// </summary>
        public const string ArgumentFileName = ".parrelsyncarg";
        
	private static bool? isCloneFileExistCache = null;

	/// <summary>
	/// Returns true if the project currently open in Unity Editor is a clone.
	/// </summary>
	/// <returns></returns>
	public static bool IsClone()
	{
		if(isCloneFileExistCache == null)
		{
			// The project is a clone if its root directory contains an empty file named ".clone".
			string cloneFilePath = Path.Combine(GetCurrentProjectPath(), CloneFileName);
			isCloneFileExistCache = File.Exists(cloneFilePath);
		}

		return isCloneFileExistCache.Value;
	}

	/// <summary>
	/// Get the path to the current unityEditor project folder's info
	/// </summary>
	/// <returns></returns>
	public static string GetCurrentProjectPath()
	{
		if(Application.isEditor)
			return new DirectoryInfo(Application.dataPath).Parent.FullName;
		else
			return Application.dataPath;
	}

	/// <summary>
	/// Get the argument of this clone project.
	/// If this is the original project, will return an empty string.
	/// </summary>
	/// <returns></returns>
	public static string GetArgument()
	{
		string argument = "";
		if(Clones.IsClone())
		{
			string argumentFilePath = Path.Combine(GetCurrentProjectPath(), ArgumentFileName);
			if(File.Exists(argumentFilePath))
			{
				argument = File.ReadAllText(argumentFilePath, System.Text.Encoding.UTF8);
			}
		}

		return argument;
	}
}
}