//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2015-2016 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Dapplo.Jira
// 
//  Dapplo.Jira is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Dapplo.Jira is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;
using Dapplo.Log.Facade;
using Dapplo.Log.Loggers;

#endregion

namespace Dapplo.Jira.WpfExample
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			LogSettings.RegisterDefaultLogger<TraceLogger>();
			DataContext = this;

			Loaded += MainWindow_Loaded;
		}

		public ObservableCollection<Project> Projects { get; set; } = new ObservableCollection<Project>();

		private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			var jiraApi = new JiraApi(new Uri("https://greenshot.atlassian.net"));

			var projects = await jiraApi.GetProjectsAsync();

			foreach (var project in projects)
			{
				// Demonstrate the Avatar and it's underlying GetAsAsync<BitmapSource>
				// could also be done with setting the source of the image, but this might not work without login
				var avatar = await jiraApi.GetAvatarAsync<BitmapSource>(project.Avatar);
				Projects.Add(new Project
				{
					Title = project.Name,
					Avatar = avatar
				});
			}
		}
	}
}