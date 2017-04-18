#region Dapplo 2017 - GNU Lesser General Public License

// Dapplo - building blocks for .NET applications
// Copyright (C) 2017 Dapplo
// 
// For more information see: http://dapplo.net/
// Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
// This file is part of Dapplo.Jira
// 
// Dapplo.Jira is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Dapplo.Jira is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have a copy of the GNU Lesser General Public License
// along with Dapplo.Jira. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#endregion

#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dapplo.HttpExtensions;
using Dapplo.Jira.Domains;
using Dapplo.Jira.Entities;
using Dapplo.Jira.Internal;

#endregion

namespace Dapplo.Jira
{
    /// <summary>
    ///     This holds all the issue related extensions methods
    /// </summary>
    public static class AgileExtensions
    {
        /// <summary>
        ///     Add comment to the specified issue
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="issueKey">key for the issue</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>AgileIssue</returns>
        public static async Task<AgileIssue> GetIssueAsync(this IAgileDomain jiraClient, string issueKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (issueKey == null)
            {
                throw new ArgumentNullException(nameof(issueKey));
            }
            jiraClient.Behaviour.MakeCurrent();
            var agileIssueUri = jiraClient.JiraAgileRestUri.AppendSegments("issue", issueKey);
            if (JiraConfig.ExpandGetIssue?.Length > 0)
            {
                agileIssueUri = agileIssueUri.ExtendQuery("expand", string.Join(",", JiraConfig.ExpandGetIssue.Concat(new[] {"customfield_10007"})));
            }
            var response = await agileIssueUri.GetAsAsync<HttpResponse<AgileIssue, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get all sprints
        ///     See: https://docs.atlassian.com/jira/REST/latest/#d2e1139
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="boardId">Id of the board to get the sprints for</param>
        /// <param name="stateFilter">
        ///     Filters results to sprints in specified states. Valid values: future, active, closed. You can
        ///     define multiple states separated by commas, e.g. state=active,closed
        /// </param>
        /// <param name="page">optional Pageable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Results with Sprint objects</returns>
        public static async Task<Results<Sprint>> GetSprintsAsync(this IAgileDomain jiraClient, long boardId, string stateFilter = null, Pageable page = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var sprintsUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId, "sprint");
            if (page != null)
            {
                sprintsUri = sprintsUri.ExtendQuery(new Dictionary<string, object>
                {
                    {
                        "startAt", page.StartAt
                    },
                    {
                        "maxResults", page.MaxResults
                    }
                });
            }
            if (stateFilter != null)
            {
                sprintsUri = sprintsUri.ExtendQuery("state", stateFilter);
            }
            var response = await sprintsUri.GetAsAsync<HttpResponse<Results<Sprint>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get backlog of a board
        ///     See
        ///     <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board-getIssuesForBacklog">
        ///         get issues for
        ///         backlog
        ///     </a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="boardId">Id of the board to get the backlog for</param>
        /// <param name="page">optional Pageable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Results with Sprint objects</returns>
        public static async Task<SearchResult<AgileIssue>> GetBacklogAsync(this IAgileDomain jiraClient, long boardId, Pageable page = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var backlogIssuesUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId, "backlog");
            if (page != null)
            {
                backlogIssuesUri = backlogIssuesUri.ExtendQuery(new Dictionary<string, object>
                {
                    {
                        "startAt", page.StartAt
                    },
                    {
                        "maxResults", page.MaxResults
                    }
                });
            }
            var response = await backlogIssuesUri.GetAsAsync<HttpResponse<SearchResult<AgileIssue>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get all issues on a board
        ///     See
        ///     <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board-getIssuesForBoard">
        ///         get issues for
        ///         board
        ///     </a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="boardId">Id of the board to get the issues for</param>
        /// <param name="page">optional Pageable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>SearchResult with AgileIssue objects</returns>
        public static async Task<SearchResult<AgileIssue>> GetIssuesOnBoardAsync(this IAgileDomain jiraClient, long boardId, Pageable page = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var boardIssuesUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId, "issue");
            if (page != null)
            {
                boardIssuesUri = boardIssuesUri.ExtendQuery(new Dictionary<string, object>
                {
                    {
                        "startAt", page.StartAt
                    },
                    {
                        "maxResults", page.MaxResults
                    }
                });
            }
            var response = await boardIssuesUri.GetAsAsync<HttpResponse<SearchResult<AgileIssue>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }


        /// <summary>
        ///     Get all issues for a spring
        ///     See
        ///     <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board/{boardId}/sprint-getIssuesForSprint">
        ///         get
        ///         issues for spring
        ///     </a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="boardId">Id of the board to get the issues for</param>
        /// <param name="sprintId">Id of the sprint to get the issues for</param>
        /// <param name="page">optional Pageable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>SearchResult with AgileIssue objects</returns>
        public static async Task<SearchResult<AgileIssue>> GetIssuesInSprintAsync(this IAgileDomain jiraClient, long boardId, long sprintId, Pageable page = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var sprintIssuesUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId, "sprint", sprintId, "issue");
            if (page != null)
            {
                sprintIssuesUri = sprintIssuesUri.ExtendQuery(new Dictionary<string, object>
                {
                    {
                        "startAt", page.StartAt
                    },
                    {
                        "maxResults", page.MaxResults
                    }
                });
            }
            var response = await sprintIssuesUri.GetAsAsync<HttpResponse<SearchResult<AgileIssue>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get board configuration
        ///     See
        ///     <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board-getConfiguration">
        ///         get board
        ///         configuration
        ///     </a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="boardId">Id of the board to get the sprints for</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>BoardConfiguration</returns>
        public static async Task<BoardConfiguration> GetBoardConfigurationAsync(this IAgileDomain jiraClient, long boardId,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var boardConfigurationUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId, "configuration");
            var response = await boardConfigurationUri.GetAsAsync<HttpResponse<BoardConfiguration, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Returns the board for the given board Id. This board will only be returned if the user has permission to view it.
        ///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board-getBoard">get board</a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="boardId">Id of the board to return</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Board</returns>
        public static async Task<Board> GetBoardAsync(this IAgileDomain jiraClient, long boardId, CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var boardUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId);
            var response = await boardUri.GetAsAsync<HttpResponse<Board, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Creates a new board. Board name, type and filter Id is required.
        ///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board-createBoard">create board</a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="board">Board to create</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task<Board> CreateBoardAsync(this IAgileDomain jiraClient, Board board, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (board == null)
            {
                throw new ArgumentNullException(nameof(board));
            }
            if (board.FilterId == 0)
            {
                throw new ArgumentNullException(nameof(board.FilterId));
            }
            jiraClient.Behaviour.MakeCurrent();
            var boardUri = jiraClient.JiraAgileRestUri.AppendSegments("board");
            var response = await boardUri.PostAsync<HttpResponse<Board>>(board, cancellationToken).ConfigureAwait(false);
            return response.HandleErrors(HttpStatusCode.Created);
        }

        /// <summary>
        ///     Deletes the board.
        ///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board-deleteBoard">delete board</a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="boardId">Id of the board to return</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public static async Task DeleteBoardAsync(this IAgileDomain jiraClient, long boardId, CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var boardUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId);
            var response = await boardUri.DeleteAsync<HttpResponse>(cancellationToken).ConfigureAwait(false);
            response.HandleStatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        ///     Get all boards
        ///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board-getAllBoards">get all boards</a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="type">Filters results to boards of the specified type. Valid values: scrum, kanban.</param>
        /// <param name="name">Filters results to boards that match or partially match the specified name.</param>
        /// <param name="projectKeyOrId">
        ///     Filters results to boards that are relevant to a project. Relevance means that the jql
        ///     filter defined in board contains a reference to a project.
        /// </param>
        /// <param name="page">optional Pageable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Results with Board objects</returns>
        public static async Task<Results<Board>> GetBoardsAsync(this IAgileDomain jiraClient, string type = null, string name = null, string projectKeyOrId = null,
            Pageable page = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var boardsUri = jiraClient.JiraAgileRestUri.AppendSegments("board");
            if (page != null)
            {
                boardsUri = boardsUri.ExtendQuery(new Dictionary<string, object>
                {
                    {
                        "startAt", page.StartAt
                    },
                    {
                        "maxResults", page.MaxResults
                    }
                });
            }
            if (type != null)
            {
                boardsUri = boardsUri.ExtendQuery("type", type);
            }
            if (name != null)
            {
                boardsUri = boardsUri.ExtendQuery("name", name);
            }
            if (projectKeyOrId != null)
            {
                boardsUri = boardsUri.ExtendQuery("projectKeyOrId", projectKeyOrId);
            }
            var response = await boardsUri.GetAsAsync<HttpResponse<Results<Board>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get all epics for a board
        ///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board/{boardId}/epic">get epics on board</a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="boardId">Id of the board to get the epics for</param>
        /// <param name="page">optional Pageable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Results with Epic objects</returns>
        public static async Task<Results<Epic>> GetEpicsAsync(this IAgileDomain jiraClient, long boardId, Pageable page = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var epicsUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId, "epic");
            if (page != null)
            {
                epicsUri = epicsUri.ExtendQuery(new Dictionary<string, object>
                {
                    {
                        "startAt", page.StartAt
                    },
                    {
                        "maxResults", page.MaxResults
                    }
                });
            }
            var response = await epicsUri.GetAsAsync<HttpResponse<Results<Epic>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get all issues for an Epic
        ///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board/{boardId}/epic-getIssuesForEpic">get issues for epic</a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="boardId">Id of the board to get the issues for</param>
        /// <param name="epicId">Id of the epic to get the issues for</param>
        /// <param name="page">optional Pageable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>SearchResult with AgileIssue objects</returns>
        public static async Task<SearchResult<AgileIssue>> GetIssuesForEpicAsync(this IAgileDomain jiraClient, long boardId, long epicId, Pageable page = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var epicIssuesUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId, "epic", epicId, "issue");
            if (page != null)
            {
                epicIssuesUri = epicIssuesUri.ExtendQuery(new Dictionary<string, object>
                {
                    {
                        "startAt", page.StartAt
                    },
                    {
                        "maxResults", page.MaxResults
                    }
                });
            }
            var response = await epicIssuesUri.GetAsAsync<HttpResponse<SearchResult<AgileIssue>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Returns all issues that belong to the epic, for the given epic Id.
        ///     This only includes issues that the user has permission to view.
        ///     Issues returned from this resource include Agile fields, like sprint, closedSprints, flagged, and epic. By default, the returned issues are ordered by rank.
        ///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/epic-getIssuesForEpic">get issues for epic</a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="epicId">Id of the epic to get the issues for</param>
        /// <param name="page">optional Pageable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>SearchResult with AgileIssue objects</returns>
        public static async Task<SearchResult<AgileIssue>> GetIssuesForEpicAsync(this IAgileDomain jiraClient, long epicId, Pageable page = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var epicIssuesUri = jiraClient.JiraAgileRestUri.AppendSegments("epic", epicId, "issue");
            if (page != null)
            {
                epicIssuesUri = epicIssuesUri.ExtendQuery(new Dictionary<string, object>
                {
                    {
                        "startAt", page.StartAt
                    },
                    {
                        "maxResults", page.MaxResults
                    }
                });
            }
            var response = await epicIssuesUri.GetAsAsync<HttpResponse<SearchResult<AgileIssue>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get an Epic
        ///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/epic-getEpic">Get epic</a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="epicId">Id of the epic to get</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Epic</returns>
        public static async Task<Epic> GetEpicAsync(this IAgileDomain jiraClient, long epicId, CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var epicUri = jiraClient.JiraAgileRestUri.AppendSegments("epic", epicId);
            var response = await epicUri.GetAsAsync<HttpResponse<Epic, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Update an Epic
        ///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/epic-partiallyUpdateEpic">Partially update epic</a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="epic">Epic to update</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Epic</returns>
        public static async Task<Epic> UpdateEpicAsync(this IAgileDomain jiraClient, Epic epic, CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var epicUri = jiraClient.JiraAgileRestUri.AppendSegments("epic", epic.Id);
            var response = await epicUri.PostAsync<HttpResponse<Epic, Error>>(epic, cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }

        /// <summary>
        ///     Get all issues without an Epic
        ///     See <a href="https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/board/{boardId}/epic-getIssuesWithoutEpic">get issues without epic</a>
        /// </summary>
        /// <param name="jiraClient">IAgileDomain to bind the extension method to</param>
        /// <param name="boardId">Id of the board to get the issues for</param>
        /// <param name="page">optional Pageable</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>SearchResult with AgileIssue objects</returns>
        public static async Task<SearchResult<AgileIssue>> GetIssuesWithoutEpicAsync(this IAgileDomain jiraClient, long boardId, Pageable page = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            jiraClient.Behaviour.MakeCurrent();
            var epicLessIssuesUri = jiraClient.JiraAgileRestUri.AppendSegments("board", boardId, "epic", "none", "issue");
            if (page != null)
            {
                epicLessIssuesUri = epicLessIssuesUri.ExtendQuery(new Dictionary<string, object>
                {
                    {
                        "startAt", page.StartAt
                    },
                    {
                        "maxResults", page.MaxResults
                    }
                });
            }
            var response = await epicLessIssuesUri.GetAsAsync<HttpResponse<SearchResult<AgileIssue>, Error>>(cancellationToken).ConfigureAwait(false);
            return response.HandleErrors();
        }
    }
}