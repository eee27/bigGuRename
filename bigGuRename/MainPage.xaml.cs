namespace bigGuRename;

public partial class MainPage : ContentPage
{
	int count = 0;

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

	private void OnTargetClicked(object sender, EventArgs e)
	{
		string originDateStr = originDate.Text;
		string targetStartDateStr=targetStartDate.Text;
		string targetEndDateStr=targetEndDate.Text;

		if (!string.IsNullOrEmpty(originDateStr)
			&& !string.IsNullOrEmpty(targetStartDateStr)
			&& !string.IsNullOrEmpty(targetEndDateStr)

			&& originDateStr.All(char.IsDigit)
			&& targetStartDateStr.All(char.IsDigit)
			&& targetEndDateStr.All(char.IsDigit)
			) 
		{


		}
		else
		{
			DisplayAlert("请检查日期格式", "日期格式有误,请确定三个日期均为纯数字且非空", "关闭");
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


}

