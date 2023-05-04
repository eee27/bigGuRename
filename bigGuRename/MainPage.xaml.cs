using System.Diagnostics;

namespace bigGuRename;

public partial class MainPage : ContentPage
{
	private static string dateFormat = "yyyyMMdd";
	private static string dateShowFormat = "yyyy/MM/dd";

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		/*
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
		*/
	}

	private async void OnTargetClicked(object sender, EventArgs e)
	{
		string originDateStr = originDate.Text;
		string targetStartDateStr = targetStartDate.Text;
		string targetEndDateStr = targetEndDate.Text;

		if (!string.IsNullOrEmpty(originDateStr)
			&& !string.IsNullOrEmpty(targetStartDateStr)
			&& !string.IsNullOrEmpty(targetEndDateStr)

			&& originDateStr.All(char.IsDigit)
			&& targetStartDateStr.All(char.IsDigit)
			&& targetEndDateStr.All(char.IsDigit)
			)
		{
			DateTime originDate;
			DateTime targetStartDate;
			DateTime targetEndDate;

			DateTime.TryParseExact(originDateStr, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out originDate);
			DateTime.TryParseExact(targetStartDateStr, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out targetStartDate);
			DateTime.TryParseExact(targetEndDateStr, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal, out targetEndDate);

			bool startAnswer = await DisplayAlert("请确认", "处理路径:" + originFilePath.Text + "\r源时间:" + originDate.ToString(dateShowFormat) + "\r起始时间:" + targetStartDate.ToString(dateShowFormat) + "\r结束时间:" + targetEndDate.ToString(dateShowFormat), "OjbK", "Nooooo");
			if (startAnswer)
			{
				DirectoryInfo dicInfo = Directory.CreateDirectory(originFilePath.Text + "target/");

				CopyFiles(originFilePath.Text, dicInfo.FullName, originDateStr, targetStartDate, targetEndDate);

				await DisplayAlert("执行完成", "执行完成,至少没报错,如有bug再说", "OjbK");
			}
		}
		else
		{
			await DisplayAlert("请检查日期格式", "日期格式有误,请确定三个日期均为纯数字且非空", "OjbK");
		}
	}

	private void OnNumTextChanged(object sender, TextChangedEventArgs e)
	{
		/*
		string oldText = e.OldTextValue;
		string newText = e.NewTextValue;

		if(newText.All(char
			.IsDigit))
		{
		}
		else
		{
		}
		*/
	}

	private static void CopyFiles(string originFolder, string targetFolder, string originDate, DateTime targetStartDate, DateTime targetEndDate)
	{
		DirectoryInfo originDicInfo = new DirectoryInfo(originFolder);
		foreach (FileSystemInfo fsInfo in originDicInfo.GetFileSystemInfos())
		{
			Debug.WriteLine("checkFile:"+fsInfo.FullName);
			if (fsInfo is FileInfo && fsInfo.Name.Contains(originDate))
			{
				Debug.WriteLine("startFile:"+fsInfo.Name);

				DateTime flagDate = targetStartDate;

				while(flagDate.CompareTo(targetEndDate.AddDays(1d))<0)
				{
					Debug.WriteLine("calc date:" + flagDate);
					string destPath = Path.Combine(targetFolder, fsInfo.Name.Replace(originDate, flagDate.ToString(dateFormat)));
					Debug.WriteLine("targetCopy:" + destPath);
					File.Copy(fsInfo.FullName, destPath, true);

					flagDate=flagDate.AddDays(1);
				}
			}
		}
	}
}