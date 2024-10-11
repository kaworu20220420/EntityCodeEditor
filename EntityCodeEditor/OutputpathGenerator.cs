namespace EntityCodeEditor
{
	public class OutputPathGenerator
	{
		public string GenerateOutputPath(string filePathBoxText)
		{
			var directoryPath = Directory.Exists(filePathBoxText) ? filePathBoxText : Path.GetDirectoryName(filePathBoxText);

			// 上の階層にModelsかModelディレクトリがあるか確認
			var parentDirectory = Directory.GetParent(directoryPath);
			if (parentDirectory != null)
			{
				var modelsDirectory = parentDirectory.GetDirectories().FirstOrDefault(d => d.Name.Equals("Models", StringComparison.OrdinalIgnoreCase) || d.Name.Equals("Model", StringComparison.OrdinalIgnoreCase));
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
	}
}
