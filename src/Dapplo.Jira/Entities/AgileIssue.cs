// Copyright (c) Dapplo and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text.Json.Serialization;
using System.Text.Json;

namespace Dapplo.Jira.Entities;

/// <summary>
///     Agile Issue information
///     See: https://docs.atlassian.com/jira-software/REST/cloud/#agile/1.0/issue-getIssue
/// </summary>
public class AgileIssue : IssueWithFields<AgileIssueFields>
{
    /// <summary>
    ///     Retrieve the sprint information, this is a hack for getting the information.
    ///     More details are <a href="https://jira.atlassian.com/browse/JSW-9928">here</a>
    ///     I created a ticket about this <a href="https://jira.atlassian.com/browse/JSW-15530">here</a>
    /// </summary>
    public Sprint Sprint
    {
        get
        {
            if (!Fields.CustomFields.ContainsKey(JiraConfig.SpintCustomField))
            {
                return null;
            }

            if (!(Fields.CustomFields[JiraConfig.SpintCustomField] is JsonElement jsonElement))
            {
                return null;
            }

            if (jsonElement.ValueKind != JsonValueKind.Array)
            {
                return null;
            }

            var jArray = jsonElement.EnumerateArray().ToArray();
            if (jArray.Length == 0)
            {
                return null;
            }

            switch (jArray[0].ValueKind)
            {
                case JsonValueKind.Object:
                    return JsonSerializer.Deserialize<Sprint>(jArray[0].GetRawText());
                case JsonValueKind.String:
                {
                    var serializedSprintInformation = jArray[0].GetString();
                    if (serializedSprintInformation == null)
                    {
                        return null;
                    }

                    var matchId = Regex.Match(serializedSprintInformation, "id=([^,]+),");
                    var matchName = Regex.Match(serializedSprintInformation, "name=([^,]+),");
                    var matchState = Regex.Match(serializedSprintInformation, "state=([^,]+),");
                    return new Sprint
                    {
                        Name = matchName.Groups[1].Value,
                        Id = int.Parse(matchId.Groups[1].Value),
                        State = matchState.Groups[1].Value
                    };
                }
            }


            return null;
        }
    }

    /// <summary>
    ///     Retrieve the estimation (story points) from the issue, this only works by using the BoardConfiguration
    ///     This is a conveniance method for the generic GetEstimation and assumes a long
    /// </summary>
    /// <param name="boardConfiguration">BoardConfiguration</param>
    /// <returns>long with estimation or 0 if nothing</returns>
    public long GetEstimation(BoardConfiguration boardConfiguration)
    {
        return GetEstimation<long>(boardConfiguration);
    }

    /// <summary>
    ///     Retrieve the estimation (story points) from the issue, this only works by using the BoardConfiguration
    /// </summary>
    /// <typeparam name="T">Type for the estimation</typeparam>
    /// <param name="boardConfiguration">BoardConfiguration</param>
    /// <returns>T or default(T) if there is no value</returns>
    public T GetEstimation<T>(BoardConfiguration boardConfiguration)
    {
        if (boardConfiguration == null)
        {
            throw new ArgumentNullException(nameof(boardConfiguration));
        }

        // Get the custom estimation field ID
        var estimationCustomField = boardConfiguration.Estimation.Field.FieldId;
        // check if there is a custom field for the ID
        if (!Fields.CustomFields.ContainsKey(estimationCustomField))
        {
            return default;
        }

        // We have a custom field, get it
        var result = Fields.CustomFields[estimationCustomField];
        // Return the custom field, as the supplied type
        return result == null ? default : (T)result;
    }

    /// <summary>
    ///     Retrieve the rank from the issue, this only works by using the BoardConfiguration
    /// </summary>
    /// <param name="boardConfiguration">BoardConfiguration</param>
    /// <returns>Something which represents a rank</returns>
    public string GetRank(BoardConfiguration boardConfiguration)
    {
        if (boardConfiguration == null)
        {
            throw new ArgumentNullException(nameof(boardConfiguration));
        }

        var rankCustomField = $"customfield_{boardConfiguration.Ranking.RankCustomFieldId}";
        if (!Fields.CustomFields.ContainsKey(rankCustomField))
        {
            return null;
        }

        return (string)Fields.CustomFields[rankCustomField];
    }
}