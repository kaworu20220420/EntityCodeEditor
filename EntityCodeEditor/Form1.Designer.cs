namespace EntityCodeEditor
{
	partial class EntityCodeEditorForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			FilePathBox = new TextBox();
			ExecuteButton = new Button();
			TimeStampRadioButton = new RadioButton();
			MoveOldFolderRadioButton = new RadioButton();
			ToTrashRadioButton = new RadioButton();
			label1 = new Label();
			DbContextTextBox = new TextBox();
			label2 = new Label();
			label3 = new Label();
			AddComment = new RadioButton();
			EntityEditor = new RadioButton();
			label4 = new Label();
			SuspendLayout();
			// 
			// FilePathBox
			// 
			FilePathBox.AllowDrop = true;
			FilePathBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			FilePathBox.Location = new Point(28, 51);
			FilePathBox.Margin = new Padding(4, 5, 4, 5);
			FilePathBox.Name = "FilePathBox";
			FilePathBox.Size = new Size(890, 30);
			FilePathBox.TabIndex = 0;
			FilePathBox.DragDrop += FilePathBoxBox_DragDrop;
			FilePathBox.DragEnter += FilePathBoxBox_DragEnter;
			// 
			// ExecuteButton
			// 
			ExecuteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			ExecuteButton.Font = new Font("Impact", 11.25F);
			ExecuteButton.Location = new Point(799, 199);
			ExecuteButton.Name = "ExecuteButton";
			ExecuteButton.Size = new Size(119, 36);
			ExecuteButton.TabIndex = 1;
			ExecuteButton.Text = "Execute";
			ExecuteButton.UseVisualStyleBackColor = true;
			ExecuteButton.Click += ExecuteButton_Click;
			// 
			// TimeStampRadioButton
			// 
			TimeStampRadioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			TimeStampRadioButton.AutoSize = true;
			TimeStampRadioButton.Checked = true;
			TimeStampRadioButton.Font = new Font("Impact", 14.25F);
			TimeStampRadioButton.Location = new Point(610, 142);
			TimeStampRadioButton.Name = "TimeStampRadioButton";
			TimeStampRadioButton.Size = new Size(150, 27);
			TimeStampRadioButton.TabIndex = 2;
			TimeStampRadioButton.TabStop = true;
			TimeStampRadioButton.Text = "Add TimeStamp";
			TimeStampRadioButton.UseVisualStyleBackColor = true;
			// 
			// MoveOldFolderRadioButton
			// 
			MoveOldFolderRadioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			MoveOldFolderRadioButton.AutoSize = true;
			MoveOldFolderRadioButton.Font = new Font("Impact", 14.25F);
			MoveOldFolderRadioButton.Location = new Point(610, 175);
			MoveOldFolderRadioButton.Name = "MoveOldFolderRadioButton";
			MoveOldFolderRadioButton.Size = new Size(170, 27);
			MoveOldFolderRadioButton.TabIndex = 3;
			MoveOldFolderRadioButton.Text = "Mode to old folder";
			MoveOldFolderRadioButton.UseVisualStyleBackColor = true;
			// 
			// ToTrashRadioButton
			// 
			ToTrashRadioButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			ToTrashRadioButton.AutoSize = true;
			ToTrashRadioButton.Font = new Font("Impact", 14.25F);
			ToTrashRadioButton.Location = new Point(610, 208);
			ToTrashRadioButton.Name = "ToTrashRadioButton";
			ToTrashRadioButton.Size = new Size(73, 27);
			ToTrashRadioButton.TabIndex = 4;
			ToTrashRadioButton.Text = "Trash";
			ToTrashRadioButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			label1.AutoSize = true;
			label1.Font = new Font("Ink Free", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label1.Location = new Point(470, 146);
			label1.Name = "label1";
			label1.Size = new Size(120, 23);
			label1.TabIndex = 5;
			label1.Text = "Original file ...";
			// 
			// DbContextTextBox
			// 
			DbContextTextBox.AllowDrop = true;
			DbContextTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			DbContextTextBox.Location = new Point(516, 101);
			DbContextTextBox.Margin = new Padding(4, 5, 4, 5);
			DbContextTextBox.Name = "DbContextTextBox";
			DbContextTextBox.Size = new Size(402, 30);
			DbContextTextBox.TabIndex = 6;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Font = new Font("Ink Free", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label2.Location = new Point(399, 103);
			label2.Name = "label2";
			label2.Size = new Size(110, 23);
			label2.TabIndex = 7;
			label2.Text = "DbContext ...";
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Font = new Font("Ink Free", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label3.Location = new Point(20, 159);
			label3.Name = "label3";
			label3.Size = new Size(101, 23);
			label3.TabIndex = 10;
			label3.Text = "Edit type ...";
			// 
			// AddComment
			// 
			AddComment.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			AddComment.AutoSize = true;
			AddComment.Font = new Font("Impact", 14.25F);
			AddComment.Location = new Point(127, 188);
			AddComment.Name = "AddComment";
			AddComment.Size = new Size(134, 27);
			AddComment.TabIndex = 9;
			AddComment.Text = "AddComment";
			AddComment.UseVisualStyleBackColor = true;
			// 
			// EntityEditor
			// 
			EntityEditor.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			EntityEditor.AutoSize = true;
			EntityEditor.Checked = true;
			EntityEditor.Font = new Font("Impact", 14.25F);
			EntityEditor.Location = new Point(127, 155);
			EntityEditor.Name = "EntityEditor";
			EntityEditor.Size = new Size(104, 27);
			EntityEditor.TabIndex = 8;
			EntityEditor.TabStop = true;
			EntityEditor.Text = "Edit Entity";
			EntityEditor.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Font = new Font("Ink Free", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label4.Location = new Point(20, 23);
			label4.Name = "label4";
			label4.Size = new Size(113, 23);
			label4.TabIndex = 11;
			label4.Text = "File full path";
			// 
			// EntityCodeEditorForm
			// 
			AllowDrop = true;
			AutoScaleDimensions = new SizeF(9F, 23F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(950, 254);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(AddComment);
			Controls.Add(EntityEditor);
			Controls.Add(label2);
			Controls.Add(DbContextTextBox);
			Controls.Add(label1);
			Controls.Add(ToTrashRadioButton);
			Controls.Add(MoveOldFolderRadioButton);
			Controls.Add(TimeStampRadioButton);
			Controls.Add(ExecuteButton);
			Controls.Add(FilePathBox);
			Font = new Font("メイリオ", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
			Margin = new Padding(4, 5, 4, 5);
			Name = "EntityCodeEditorForm";
			Text = "EntityCodeEditor";
			DragDrop += FilePathBoxBox_DragDrop;
			DragEnter += FilePathBoxBox_DragEnter;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox FilePathBox;
		private Button ExecuteButton;
		private RadioButton TimeStampRadioButton;
		private RadioButton MoveOldFolderRadioButton;
		private RadioButton ToTrashRadioButton;
		private Label label1;
		private TextBox DbContextTextBox;
		private Label label2;
		private Label label3;
		private RadioButton AddComment;
		private RadioButton EntityEditor;
		private Label label4;
	}
}
