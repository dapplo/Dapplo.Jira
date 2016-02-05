/*
	Dapplo - building blocks for desktop applications
	Copyright (C) 2015-2016 Dapplo

	For more information see: http://dapplo.net/
	Dapplo repositories are hosted on GitHub: https://github.com/dapplo

	This file is part of Dapplo.Jira

	Dapplo.Jira is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.

	Dapplo.Jira is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/>.
 */

using Dapplo.LogFacade;
using Dapplo.LogFacade.Loggers;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Dapplo.Jira.WpfExample
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public ObservableCollection<Project> Projects
		{
			get;
			set;
		} = new ObservableCollection<Project>();

		public MainWindow()
		{
			InitializeComponent();
			LogSettings.Logger = new TraceLogger();
			DataContext = this;

			Loaded += MainWindow_Loaded;
		}

		private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			var jiraApi = await JiraApi.CreateAndInitializeAsync(new Uri("https://greenshot.atlassian.net"));

			var projects = await jiraApi.Projects();

			foreach (var project in projects)
			{
				// Demonstrate the Avatar and it's underlying GetAsAsync<BitmapSource>
				// could also be done with setting the source of the image, but this might not work without login
				var avatar = await jiraApi.Avatar<BitmapSource>(project);
				Projects.Add(new Project
				{
					Title = project.name,
					Avatar = avatar
				});
			}
		}
	}
}
