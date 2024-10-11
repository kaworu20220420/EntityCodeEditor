namespace EntityCodeEditor
{
	public class OutputPathGenerator
	{
		/// <summary>
		/// 出力先のフォルダを生成する
		/// </summary>
		/// <param name="filePathBoxText">元のファイル・フォルダのフルパス</param>
		/// <returns>新規のファイルを保存するフォルダのフルパス</returns>
		public string GenerateOutputPath(string filePathBoxText)
		{
			var directoryPath = Directory.Exists(filePathBoxText) ? filePathBoxText : Path.GetDirectoryName(filePathBoxText);

			// 上の階層にModelsかModelディレクトリがあるか確認
			var parentDirectory = Directory.GetParent(directoryPath);
			if (parentDirectory != null)
			{
				var modelsDirectory = parentDirectory.GetDirectories()
					.FirstOrDefault(d => d.Name.Equals("Models", StringComparison.OrdinalIgnoreCase) || d.Name.Equals("Model", StringComparison.OrdinalIgnoreCase));
				if (modelsDirectory != null)
				{
					var repositoryPath = Path.Combine(modelsDirectory.FullName, "Repository");
					if (!Directory.Exists(repositoryPath))
					{
						Directory.CreateDirectory(repositoryPath);
					}
					return repositoryPath;
				}
			}

			// 現在のパスがディレクトリの場合
			if (Directory.Exists(filePathBoxText))
			{
				var repositoryPath = Path.Combine(filePathBoxText, "Repository");
				if (!Directory.Exists(repositoryPath))
				{
					Directory.CreateDirectory(repositoryPath);
				}
				return repositoryPath;
			}

			// 現在のパスがファイルの場合
			return directoryPath;
		}

		public string SetCherckProgramcsPath(string filePathBoxText)
		{
			var directoryPath = Path.GetDirectoryName(filePathBoxText);

			// 上の階層にModelsかModelディレクトリがあるか確認
			var parentDirectory = Directory.GetParent(directoryPath);
			if (parentDirectory != null)
			{
				var programCs = parentDirectory.GetFiles()
					.FirstOrDefault(d => d.Name.Equals("Program.cs", StringComparison.OrdinalIgnoreCase))?.FullName;
				return programCs;
			}
			return string.Empty;
		}
	}
}
