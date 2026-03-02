# JIRA REST API Coverage Analysis for Dapplo.Jira

This document provides a comprehensive analysis of the JIRA REST API coverage in the Dapplo.Jira library. It lists functionality that is currently unsupported, deprecated, or missing fields, with links to both Atlassian documentation and the corresponding files in this project.

**Purpose:** This document can be used to create feature requests for implementing missing functionality or updating deprecated endpoints.

**Last Updated:** 2026-01-29

---

## Table of Contents

1. [Summary](#summary)
2. [Currently Supported Features](#currently-supported-features)
3. [Missing JIRA Cloud REST API Endpoints](#missing-jira-cloud-rest-api-endpoints)
4. [Deprecated/Obsolete Endpoints in Dapplo.Jira](#deprecatedobsolete-endpoints-in-dapplojira)
5. [Partially Implemented Features (Missing Fields/Operations)](#partially-implemented-features-missing-fieldsoperations)
6. [Feature Request Templates](#feature-request-templates)

---

## Summary

Dapplo.Jira is a REST-based JIRA client that provides good coverage for basic JIRA operations but lacks support for many advanced features introduced in recent JIRA Cloud API versions.

**Coverage Estimate:**
- **Issues:** ~70% (CRUD, comments, transitions, search, assignments)
- **Projects:** ~40% (basic CRUD, components, versions)
- **Users:** ~60% (search, get user info)
- **Agile/Boards:** ~50% (boards, sprints, epics, backlog)
- **Filters:** ~80% (CRUD operations)
- **Attachments:** ~90% (CRUD operations)
- **Worklogs:** ~90% (CRUD operations)
- **Advanced Features:** ~10% (webhooks, automation, permissions, etc.)

---

## Currently Supported Features

Based on analysis of the codebase, the following features are currently implemented:

### Issues ([IssueDomainExtensions.cs](src/Dapplo.Jira/IssueDomainExtensions.cs))
- ✅ Get issue information
- ✅ Search issues with JQL
- ✅ Create, edit, delete issues
- ✅ Add, update comments
- ✅ Get issue transitions
- ✅ Execute issue transitions
- ✅ Create, get, delete issue links
- ✅ Get issue link types
- ✅ Get issue types
- ✅ Assign issues to users
- ✅ Get assignable users

### Projects ([ProjectDomainExtensions.cs](src/Dapplo.Jira/ProjectDomainExtensions.cs))
- ✅ Get project information
- ✅ Get all visible projects
- ✅ Get, create, update, delete components
- ✅ Get security levels
- ✅ Get project versions
- ✅ Get issue creators for project

### Users ([UserDomainExtensions.cs](src/Dapplo.Jira/UserDomainExtensions.cs))
- ✅ Get user by username or account ID
- ✅ Search users
- ✅ Get current user (myself)
- ✅ Get assignable users for projects/issues

### Agile/Boards ([AgileDomainExtensions.cs](src/Dapplo.Jira/AgileDomainExtensions.cs))
- ✅ Get boards, create, delete boards
- ✅ Get board configuration
- ✅ Get sprints for boards
- ✅ Get issues on board, backlog, sprint
- ✅ Get epics on board
- ✅ Get issues for epic
- ✅ Update epic

### Attachments ([AttachmentDomainExtensions.cs](src/Dapplo.Jira/AttachmentDomainExtensions.cs))
- ✅ Attach content to issues
- ✅ Delete attachments
- ✅ Get attachment content and thumbnails

### Worklogs ([WorkLogDomainExtensions.cs](src/Dapplo.Jira/WorkLogDomainExtensions.cs))
- ✅ Get worklogs for issue
- ✅ Create, update, delete worklogs
- ✅ Get updated worklogs

### Filters ([FilterDomainExtensions.cs](src/Dapplo.Jira/FilterDomainExtensions.cs))
- ✅ Get filter favorites
- ✅ Get user filters
- ✅ Create, update, delete filters

### Server ([ServerDomainExtensions.cs](src/Dapplo.Jira/ServerDomainExtensions.cs))
- ✅ Get server information
- ✅ Get server configuration
- ✅ Get fields
- ✅ Get avatars

### Session ([SessionDomainExtensions.cs](src/Dapplo.Jira/SessionDomainExtensions.cs))
- ⚠️ Start/end session (DEPRECATED by Atlassian)

---

## Missing JIRA Cloud REST API Endpoints

The following JIRA Cloud REST API endpoints are **NOT** currently implemented in Dapplo.Jira:

### 1. **Issue Operations** (High Priority)

#### Issue Watchers
**Status:** Not Implemented  
**Atlassian Docs:** [Issue Watchers API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-watchers/)  
**Missing Methods:**
- `GET /rest/api/3/issue/{issueIdOrKey}/watchers` - Get issue watchers
- `POST /rest/api/3/issue/{issueIdOrKey}/watchers` - Add watcher
- `DELETE /rest/api/3/issue/{issueIdOrKey}/watchers` - Remove watcher

**Feature Request:**
```
Add support for JIRA Issue Watchers API to allow:
- Retrieving list of users watching an issue
- Adding watchers to an issue
- Removing watchers from an issue

Implementation should be in IssueDomainExtensions.cs following the existing pattern.
```

#### Issue Votes
**Status:** Not Implemented  
**Atlassian Docs:** [Issue Votes API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-votes/)  
**Missing Methods:**
- `GET /rest/api/3/issue/{issueIdOrKey}/votes` - Get votes
- `POST /rest/api/3/issue/{issueIdOrKey}/votes` - Add vote
- `DELETE /rest/api/3/issue/{issueIdOrKey}/votes` - Delete vote

**Feature Request:**
```
Add support for JIRA Issue Votes API to allow:
- Retrieving vote count and voters for an issue
- Voting on issues
- Removing vote from issues

Implementation should be in IssueDomainExtensions.cs following the existing pattern.
```

#### Issue Properties
**Status:** Not Implemented  
**Atlassian Docs:** [Issue Properties API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-properties/)  
**Missing Methods:**
- `GET /rest/api/3/issue/{issueIdOrKey}/properties` - Get issue properties
- `GET /rest/api/3/issue/{issueIdOrKey}/properties/{propertyKey}` - Get issue property
- `PUT /rest/api/3/issue/{issueIdOrKey}/properties/{propertyKey}` - Set issue property
- `DELETE /rest/api/3/issue/{issueIdOrKey}/properties/{propertyKey}` - Delete issue property

**Feature Request:**
```
Add support for JIRA Issue Properties API to allow:
- Getting all properties for an issue
- Getting specific property values
- Setting custom properties on issues
- Deleting issue properties

This is useful for storing custom metadata on issues.
Implementation should be in IssueDomainExtensions.cs.
```

#### Issue Remote Links
**Status:** Not Implemented  
**Atlassian Docs:** [Issue Remote Links API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-remote-links/)  
**Missing Methods:**
- `GET /rest/api/3/issue/{issueIdOrKey}/remotelink` - Get remote issue links
- `POST /rest/api/3/issue/{issueIdOrKey}/remotelink` - Create remote issue link
- `PUT /rest/api/3/issue/{issueIdOrKey}/remotelink/{linkId}` - Update remote issue link
- `DELETE /rest/api/3/issue/{issueIdOrKey}/remotelink/{linkId}` - Delete remote issue link

**Feature Request:**
```
Add support for JIRA Remote Links API to allow:
- Creating links to external systems (URLs, applications)
- Retrieving remote links for issues
- Updating and deleting remote links

Implementation should be in IssueDomainExtensions.cs.
```

#### Issue Bulk Operations
**Status:** Not Implemented  
**Atlassian Docs:** [Issue Bulk Operations](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issues/#api-rest-api-3-issue-bulk-post)  
**Missing Methods:**
- `POST /rest/api/3/issue/bulk` - Bulk create issues
- `POST /rest/api/3/issue/archive` - Archive issues
- `POST /rest/api/3/issue/unarchive` - Unarchive issues

**Feature Request:**
```
Add support for JIRA Bulk Operations to allow:
- Creating multiple issues in a single API call
- Archiving multiple issues
- Unarchiving issues

This improves performance when dealing with multiple issues.
Implementation should be in IssueDomainExtensions.cs.
```

#### Issue Changelog
**Status:** Not Implemented  
**Atlassian Docs:** [Issue Changelog API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issues/#api-rest-api-3-issue-issueidorkey-changelog-get)  
**Missing Methods:**
- `GET /rest/api/3/issue/{issueIdOrKey}/changelog` - Get changelogs

**Feature Request:**
```
Add support for retrieving issue changelog/history to track:
- Field changes over time
- Who made what changes
- When changes occurred

Implementation should be in IssueDomainExtensions.cs.
```

### 2. **Project Operations** (High Priority)

#### Project Roles
**Status:** Not Implemented  
**Atlassian Docs:** [Project Roles API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-project-roles/)  
**Missing Methods:**
- `GET /rest/api/3/project/{projectIdOrKey}/role` - Get project roles
- `GET /rest/api/3/project/{projectIdOrKey}/role/{id}` - Get project role details
- `POST /rest/api/3/project/{projectIdOrKey}/role/{id}` - Add actors to project role
- `DELETE /rest/api/3/project/{projectIdOrKey}/role/{id}` - Remove actors from project role

**Feature Request:**
```
Add support for JIRA Project Roles API to:
- List all roles in a project
- Get role details and actors
- Assign users/groups to project roles
- Remove users/groups from project roles

Implementation should be in ProjectDomainExtensions.cs.
```

#### Project Permissions
**Status:** Not Implemented  
**Atlassian Docs:** [Project Permissions API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-project-permission-schemes/)  
**Missing Methods:**
- `GET /rest/api/3/project/{projectIdOrKey}/permissionscheme` - Get project permission scheme
- `PUT /rest/api/3/project/{projectIdOrKey}/permissionscheme` - Assign permission scheme

**Feature Request:**
```
Add support for Project Permission Schemes to:
- Get permission scheme assigned to a project
- Assign permission schemes to projects

Implementation should be in ProjectDomainExtensions.cs.
```

#### Project Properties
**Status:** Not Implemented  
**Atlassian Docs:** [Project Properties API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-project-properties/)  
**Missing Methods:**
- `GET /rest/api/3/project/{projectIdOrKey}/properties` - Get project properties
- `GET /rest/api/3/project/{projectIdOrKey}/properties/{propertyKey}` - Get property
- `PUT /rest/api/3/project/{projectIdOrKey}/properties/{propertyKey}` - Set property
- `DELETE /rest/api/3/project/{projectIdOrKey}/properties/{propertyKey}` - Delete property

**Feature Request:**
```
Add support for Project Properties API to:
- Store custom metadata on projects
- Retrieve project properties
- Update and delete properties

Implementation should be in ProjectDomainExtensions.cs.
```

#### Project Categories
**Status:** Not Implemented  
**Atlassian Docs:** [Project Categories API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-project-categories/)  
**Missing Methods:**
- `GET /rest/api/3/projectCategory` - Get all project categories
- `GET /rest/api/3/projectCategory/{id}` - Get project category
- `POST /rest/api/3/projectCategory` - Create project category
- `PUT /rest/api/3/projectCategory/{id}` - Update project category
- `DELETE /rest/api/3/projectCategory/{id}` - Delete project category

**Feature Request:**
```
Add support for Project Categories to:
- Organize projects into categories
- Create, read, update, delete project categories

Implementation should be in ProjectDomainExtensions.cs.
```

#### Project Avatars
**Status:** Not Implemented  
**Atlassian Docs:** [Project Avatars API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-project-avatars/)  
**Missing Methods:**
- `GET /rest/api/3/project/{projectIdOrKey}/avatars` - Get all project avatars
- `POST /rest/api/3/project/{projectIdOrKey}/avatar2` - Load project avatar
- `DELETE /rest/api/3/project/{projectIdOrKey}/avatar/{id}` - Delete project avatar

**Feature Request:**
```
Add support for Project Avatar management to:
- Get all avatars for a project
- Upload custom avatars
- Delete project avatars

Implementation should be in ProjectDomainExtensions.cs.
```

#### Project Types
**Status:** Not Implemented  
**Atlassian Docs:** [Project Types API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-project-types/)  
**Missing Methods:**
- `GET /rest/api/3/project/type` - Get all project types
- `GET /rest/api/3/project/type/{projectTypeKey}` - Get project type by key

**Feature Request:**
```
Add support for retrieving Project Types (software, business, service_desk).

Implementation should be in ProjectDomainExtensions.cs.
```

### 3. **User Operations** (Medium Priority)

#### User Groups
**Status:** Not Implemented  
**Atlassian Docs:** [Groups API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-groups/)  
**Missing Methods:**
- `GET /rest/api/3/groups/picker` - Find groups
- `GET /rest/api/3/group` - Get group
- `POST /rest/api/3/group` - Create group
- `DELETE /rest/api/3/group` - Remove group
- `GET /rest/api/3/group/member` - Get users from group
- `POST /rest/api/3/group/user` - Add user to group
- `DELETE /rest/api/3/group/user` - Remove user from group

**Feature Request:**
```
Add support for JIRA Groups API to:
- Search and find groups
- Create and delete groups
- Manage group membership
- Get users from groups

Implementation should be in UserDomainExtensions.cs or new GroupDomainExtensions.cs.
```

#### User Properties
**Status:** Not Implemented  
**Atlassian Docs:** [User Properties API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-user-properties/)  
**Missing Methods:**
- `GET /rest/api/3/user/properties` - Get user properties
- `GET /rest/api/3/user/properties/{propertyKey}` - Get user property
- `PUT /rest/api/3/user/properties/{propertyKey}` - Set user property
- `DELETE /rest/api/3/user/properties/{propertyKey}` - Delete user property

**Feature Request:**
```
Add support for User Properties to:
- Store custom metadata on users
- Retrieve user properties
- Update and delete user properties

Implementation should be in UserDomainExtensions.cs.
```

#### User Permissions
**Status:** Not Implemented  
**Atlassian Docs:** [User Permissions API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-user-permissions/)  
**Missing Methods:**
- `GET /rest/api/3/mypermissions` - Get my permissions
- `GET /rest/api/3/permissions` - Get all permissions
- `POST /rest/api/3/permissions/check` - Get bulk permissions

**Feature Request:**
```
Add support for User Permissions API to:
- Check user permissions for specific contexts
- Get all available permissions
- Bulk check permissions

Implementation should be in UserDomainExtensions.cs or new PermissionDomainExtensions.cs.
```

### 4. **Workflow Operations** (Medium Priority)

**Status:** Not Implemented  
**Atlassian Docs:** [Workflows API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-workflows/)  
**Missing Methods:**
- `GET /rest/api/3/workflow/search` - Get workflows with pagination
- `GET /rest/api/3/workflow/{workflowId}` - Get workflow
- `POST /rest/api/3/workflow` - Create workflow
- `DELETE /rest/api/3/workflow/{entityId}` - Delete workflow
- `GET /rest/api/3/workflow/transitions/{transitionId}/properties` - Get workflow transition properties

**Feature Request:**
```
Add support for JIRA Workflows API to:
- Search and retrieve workflows
- Create custom workflows
- Delete workflows
- Manage workflow transitions and properties

This would enable programmatic workflow management.
Implementation should be in new WorkflowDomainExtensions.cs.
```

### 5. **Dashboard Operations** (Medium Priority)

**Status:** Not Implemented  
**Atlassian Docs:** [Dashboards API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-dashboards/)  
**Missing Methods:**
- `GET /rest/api/3/dashboard` - Get all dashboards
- `GET /rest/api/3/dashboard/{id}` - Get dashboard
- `POST /rest/api/3/dashboard` - Create dashboard
- `PUT /rest/api/3/dashboard/{id}` - Update dashboard
- `DELETE /rest/api/3/dashboard/{id}` - Delete dashboard
- `GET /rest/api/3/dashboard/{dashboardId}/items/{itemId}/properties` - Get dashboard item properties

**Feature Request:**
```
Add support for JIRA Dashboards API to:
- List user dashboards
- Create, update, delete dashboards
- Manage dashboard items and gadgets

Implementation should be in new DashboardDomainExtensions.cs.
```

### 6. **Screen Operations** (Low Priority)

**Status:** Not Implemented  
**Atlassian Docs:** [Screens API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-screens/)  
**Missing Methods:**
- `GET /rest/api/3/screens` - Get all screens
- `POST /rest/api/3/screens` - Create screen
- `PUT /rest/api/3/screens/{screenId}` - Update screen
- `DELETE /rest/api/3/screens/{screenId}` - Delete screen
- `GET /rest/api/3/screens/{screenId}/availableFields` - Get available screen fields

**Feature Request:**
```
Add support for JIRA Screens API to programmatically:
- Manage screens
- Configure screen fields
- Associate screens with issue types

Implementation should be in new ScreenDomainExtensions.cs.
```

### 7. **Notification Operations** (Low Priority)

**Status:** Not Implemented  
**Atlassian Docs:** [Issue Notification Schemes API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-issue-notification-schemes/)  
**Missing Methods:**
- `GET /rest/api/3/notificationscheme` - Get notification schemes
- `GET /rest/api/3/notificationscheme/{id}` - Get notification scheme
- `POST /rest/api/3/issue/{issueIdOrKey}/notify` - Send notification for issue

**Feature Request:**
```
Add support for Notification Schemes to:
- List notification schemes
- Send custom notifications for issues

Implementation should be in new NotificationDomainExtensions.cs.
```

### 8. **Webhooks** (High Priority)

**Status:** Not Implemented  
**Atlassian Docs:** [Webhooks API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-webhooks/)  
**Missing Methods:**
- `GET /rest/api/3/webhook` - Get dynamic webhooks
- `POST /rest/api/3/webhook` - Register webhook
- `DELETE /rest/api/3/webhook` - Delete webhook
- `PUT /rest/api/3/webhook/refresh` - Extend webhook life

**Feature Request:**
```
Add support for JIRA Webhooks API to:
- Register webhooks for issue/project events
- List registered webhooks
- Delete webhooks
- Refresh webhook lifecycles

This enables real-time integration with external systems.
Implementation should be in new WebhookDomainExtensions.cs.
```

### 9. **Application Properties** (Low Priority)

**Status:** Not Implemented  
**Atlassian Docs:** [Application Properties API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-app-properties/)  
**Missing Methods:**
- `GET /rest/api/3/app/properties` - Get app properties
- `GET /rest/api/3/app/properties/{propertyKey}` - Get app property
- `PUT /rest/api/3/app/properties/{propertyKey}` - Set app property
- `DELETE /rest/api/3/app/properties/{propertyKey}` - Delete app property

**Feature Request:**
```
Add support for Application Properties to store app-specific configuration.

Implementation should be in new AppPropertiesDomainExtensions.cs.
```

### 10. **Auditing** (Low Priority)

**Status:** Not Implemented  
**Atlassian Docs:** [Audit Records API](https://developer.atlassian.com/cloud/jira/platform/rest/v3/api-group-audit-records/)  
**Missing Methods:**
- `GET /rest/api/3/auditing/record` - Get audit records

**Feature Request:**
```
Add support for retrieving audit records to track:
- User actions
- System changes
- Security events

Implementation should be in new AuditDomainExtensions.cs.
```

### 11. **Agile/Software (Advanced)** (Medium Priority)

#### Sprint Management
**Status:** Partially Implemented  
**Atlassian Docs:** [Software Sprints API](https://developer.atlassian.com/cloud/jira/software/rest/api-group-sprint/)  
**Missing Methods:**
- `POST /rest/agile/1.0/sprint` - Create sprint
- `PUT /rest/agile/1.0/sprint/{sprintId}` - Update sprint
- `DELETE /rest/agile/1.0/sprint/{sprintId}` - Delete sprint
- `POST /rest/agile/1.0/sprint/{sprintId}/issue` - Move issues to sprint
- `POST /rest/agile/1.0/sprint/{sprintId}/swap` - Swap sprint position

**Feature Request:**
```
Add support for Sprint Management operations to:
- Create new sprints
- Update sprint details (name, dates, goals)
- Delete sprints
- Move issues between sprints

Current implementation only supports reading sprint information.
Implementation should be in AgileDomainExtensions.cs.
```

#### Board Settings
**Status:** Partially Implemented  
**Atlassian Docs:** [Software Boards API](https://developer.atlassian.com/cloud/jira/software/rest/api-group-board/)  
**Missing Methods:**
- `PUT /rest/agile/1.0/board/{boardId}` - Update board (only name)
- `GET /rest/agile/1.0/board/{boardId}/properties` - Get board properties
- `PUT /rest/agile/1.0/board/{boardId}/properties/{propertyKey}` - Set board property

**Feature Request:**
```
Add support for advanced Board operations:
- Update board settings
- Manage board properties

Implementation should be in AgileDomainExtensions.cs.
```

### 12. **Service Management (JSM)** (Low Priority)

**Status:** Not Implemented  
**Atlassian Docs:** [Jira Service Management API](https://developer.atlassian.com/cloud/jira/service-desk/rest/intro/)  
**Missing:** Entire JSM/Service Desk API

**Feature Request:**
```
Add support for JIRA Service Management API including:
- Request Types
- SLA management
- Customer portal operations
- Queues
- Organizations

This would require a new module or domain extensions.
```

### 13. **Advanced Roadmaps (Plans)** (Low Priority)

**Status:** Not Implemented  
**Atlassian Docs:** Advanced Roadmaps does not have a public REST API  
**Note:** Limited API availability

**Feature Request:**
```
Monitor Atlassian's roadmap for public API for Advanced Roadmaps/Plans.
Currently not possible to implement.
```

---

## Deprecated/Obsolete Endpoints in Dapplo.Jira

The following endpoints used in Dapplo.Jira are **deprecated** by Atlassian and should be updated or removed:

### 1. **Session-based Authentication** ⚠️ DEPRECATED

**File:** [SessionDomainExtensions.cs](src/Dapplo.Jira/SessionDomainExtensions.cs)  
**Status:** Marked as Obsolete in code  
**Atlassian Notice:** [Deprecation Notice](https://developer.atlassian.com/cloud/jira/platform/deprecation-notice-basic-auth-and-cookie-based-auth/)  
**Deprecation Date:** June 2019  
**Removal Date:** Already removed from Cloud

**Current Implementation:**
```csharp
[Obsolete("This method is deprecated, see https://developer.atlassian.com/cloud/jira/platform/deprecation-notice-basic-auth-and-cookie-based-auth/")]
public static async Task<LoginInfo> StartAsync(this ISessionDomain jiraClient, string username, string password, ...)
```

**Issue:**
- Cookie-based authentication is no longer supported in JIRA Cloud
- Still works in JIRA Server/Data Center but deprecated

**Recommendation:**
```
DEPRECATION NOTICE: Session-based authentication (cookie-based) is deprecated.

For JIRA Cloud: 
- Use API tokens with Basic Auth
- Use OAuth 2.0 for apps
- Remove or clearly mark SessionDomain as Server/DC only

For JIRA Server/Data Center:
- Keep with clear deprecation warnings
- Document migration path to token-based auth

Files to update:
- src/Dapplo.Jira/SessionDomainExtensions.cs (add more prominent warnings)
- Update README.md with authentication best practices
```

### 2. **Greenhopper API** ⚠️ SEMI-DEPRECATED

**File:** [GreenhopperDomainExtensions.cs](src/Dapplo.Jira/GreenhopperDomainExtensions.cs)  
**Status:** Undocumented/Legacy API  
**Atlassian Status:** Not officially deprecated but superseded by Software API

**Current Implementation:**
```csharp
/// <summary>
///     This is a <i>not documented endpoint</i>, and could fail with every update.
///     The Greenhopper API is a leftover from when the agile functionality was not yet integrated.
///     Unfortunately they never finished the integration on the API level.
/// </summary>
```

**Issue:**
- Uses undocumented `/rest/greenhopper/1.0/` endpoints
- Could break with any JIRA update
- Some functionality not available in official Agile API

**Recommendation:**
```
MIGRATION NOTICE: Greenhopper API uses undocumented endpoints.

Actions:
1. Document which Greenhopper endpoints are still necessary
2. Try to migrate functionality to official Agile/Software API where possible
3. Keep Greenhopper domain for features not yet in official API
4. Add clear warnings about instability

Currently only GetSprintReportAsync uses Greenhopper.
Consider adding similar functionality using official APIs if available.
```

### 3. **API Version 2 Endpoints** ⚠️ SOME DEPRECATED

**Files:** Multiple files using `/rest/api/2/` endpoints  
**Status:** JIRA Cloud is moving to v3, but v2 still supported  
**Atlassian Notice:** [API v2 to v3 Migration](https://developer.atlassian.com/cloud/jira/platform/deprecation-notice-user-privacy-api-migration-guide/)

**Issue:**
- Some v2 endpoints are deprecated (especially user-related)
- Library uses mix of v2 and v3 endpoints

**Affected Areas:**
- User endpoints (username vs accountId)
- Some issue search features

**Recommendation:**
```
API VERSION AUDIT: Review all REST API endpoints

Actions:
1. Audit all uses of /rest/api/2/ endpoints
2. Migrate to /rest/api/3/ where v2 is deprecated
3. Support both v2 and v3 for backward compatibility
4. Add configuration to select API version preference

Priority endpoints to migrate:
- User operations (already partially migrated with accountId support)
- Issue search (some features v3-only)
- Deprecated fields and operations
```

### 4. **Search POST Endpoint Migration**

**File:** [IssueDomainExtensions.cs](src/Dapplo.Jira/IssueDomainExtensions.cs) (Line 213)  
**Status:** Recently Migrated ✅  
**Note in Code:** "Use API v3 for search endpoint as v2 was removed"

**Current Implementation:**
```csharp
// Use API v3 for search endpoint as v2 was removed (https://developer.atlassian.com/changelog/#CHANGE-2046)
var searchUri = jiraClient.JiraV3RestUri.AppendSegments("search", "jql");
```

**Good Practice:**
- Already migrated to v3
- Good example of handling deprecated endpoints
- Documents the reason for migration

---

## Partially Implemented Features (Missing Fields/Operations)

### 1. **Issues - Missing Fields and Operations**

**File:** [IssueDomainExtensions.cs](src/Dapplo.Jira/IssueDomainExtensions.cs)

#### Missing Operations:
- **Archive/Unarchive Issues** - API available but not implemented
- **Issue Notification** - Cannot send custom notifications
- **Issue Export** - No support for exporting issues (e.g., to XML/Word)
- **Get Edit Metadata** - `/rest/api/3/issue/{issueIdOrKey}/editmeta` not implemented
- **Get Create Metadata** - Partial support, could be enhanced

#### Missing Fields in Issue Entities:
While the library uses dynamic fields, common fields that may not be explicitly modeled:
- Issue security level (partially supported)
- Time tracking details (aggregate time spent, etc.)
- Issue resolution date
- Parent issue for subtasks (may be in custom fields)
- Archived status

**Feature Request:**
```
Enhance Issue operations with:
1. Archive/Unarchive methods
2. Get edit/create metadata helpers
3. Explicit models for commonly used fields
4. Better time tracking field support

Files to update:
- src/Dapplo.Jira/IssueDomainExtensions.cs
- src/Dapplo.Jira/Entities/Issue*.cs
```

### 2. **Projects - Missing CRUD Operations**

**File:** [ProjectDomainExtensions.cs](src/Dapplo.Jira/ProjectDomainExtensions.cs)

#### Missing Operations:
- **Create Project** - No method to create new projects
- **Update Project** - Cannot update project details
- **Delete Project** - Cannot delete projects
- **Archive Project** - No archive support
- **Get Project Statuses** - Cannot retrieve project-specific statuses

#### Missing Version Operations:
- **Create Version** - Cannot create new versions/releases
- **Update Version** - Cannot update version details
- **Delete Version** - Cannot delete versions
- **Move Version** - Cannot reorder versions

**Feature Request:**
```
Add full CRUD support for Projects:
1. CreateProjectAsync - Create new projects
2. UpdateProjectAsync - Update project details
3. DeleteProjectAsync - Delete projects
4. GetProjectStatusesAsync - Get project-specific statuses

Add Version CRUD operations:
1. CreateVersionAsync
2. UpdateVersionAsync  
3. DeleteVersionAsync
4. MoveVersionAsync

Files to update:
- src/Dapplo.Jira/ProjectDomainExtensions.cs
```

### 3. **Users - Limited Search Options**

**File:** [UserDomainExtensions.cs](src/Dapplo.Jira/UserDomainExtensions.cs)

#### Missing Operations:
- **Find Users by Email** - Limited email search
- **Find Users by Display Name** - Limited name search
- **Bulk User Operations** - No bulk get users
- **User Avatars** - No upload/manage user avatars
- **User Preferences** - Cannot get/set user preferences
- **User Columns** - Cannot get user's column configuration

**Feature Request:**
```
Enhance User operations:
1. Advanced search with email/displayName filters
2. Bulk user retrieval
3. User avatar management
4. User preference operations

Files to update:
- src/Dapplo.Jira/UserDomainExtensions.cs
```

### 4. **Agile - Limited Sprint/Epic Management**

**File:** [AgileDomainExtensions.cs](src/Dapplo.Jira/AgileDomainExtensions.cs)

#### Missing Operations:
- **Create Sprint** - Can only read sprints
- **Update Sprint** - Cannot modify sprint details
- **Delete Sprint** - Cannot remove sprints
- **Move Issues to Sprint** - No dedicated method
- **Start/Complete Sprint** - No sprint lifecycle management
- **Create Epic** - Can only read and update epics
- **Delete Epic** - Cannot remove epics
- **Rank Issues** - Cannot reorder issues on board

**Feature Request:**
```
Add full Sprint lifecycle management:
1. CreateSprintAsync
2. UpdateSprintAsync
3. DeleteSprintAsync
4. StartSprintAsync / CompleteSprintAsync
5. MoveIssuesToSprintAsync

Add Epic CRUD:
1. CreateEpicAsync
2. DeleteEpicAsync

Add Issue ranking:
1. RankIssueAsync

Files to update:
- src/Dapplo.Jira/AgileDomainExtensions.cs
```

### 5. **Filters - Limited Sharing Options**

**File:** [FilterDomainExtensions.cs](src/Dapplo.Jira/FilterDomainExtensions.cs)

#### Missing Operations:
- **Share Filter** - Cannot manage filter sharing/permissions
- **Get Filter Columns** - Cannot retrieve filter column configuration
- **Get Default Share Scope** - Cannot get default sharing settings

**Feature Request:**
```
Add Filter sharing management:
1. GetFilterSharePermissionsAsync
2. AddFilterSharePermissionAsync
3. RemoveFilterSharePermissionAsync
4. GetFilterColumnsAsync

Files to update:
- src/Dapplo.Jira/FilterDomainExtensions.cs
```

### 6. **Comments - Missing Operations**

**File:** [IssueDomainExtensions.cs](src/Dapplo.Jira/IssueDomainExtensions.cs)

#### Missing Operations:
- **Get Comments** - Can add/update but not list all comments
- **Get Single Comment** - Cannot retrieve specific comment by ID
- **Delete Comment** - Cannot delete comments

**Feature Request:**
```
Add complete Comment CRUD operations:
1. GetCommentsAsync - List all comments for an issue
2. GetCommentAsync - Get specific comment
3. DeleteCommentAsync - Delete a comment

Files to update:
- src/Dapplo.Jira/IssueDomainExtensions.cs
```

### 7. **Server - Missing Admin Operations**

**File:** [ServerDomainExtensions.cs](src/Dapplo.Jira/ServerDomainExtensions.cs)

#### Missing Operations:
- **Application Properties** - Limited app property management
- **System Information** - Limited system details
- **License Information** - Cannot get license details
- **User Management** - No bulk user operations

**Feature Request:**
```
Add Server/Admin operations:
1. GetApplicationPropertiesAsync
2. GetLicenseInformationAsync
3. System health checks
4. Bulk user management

Files to update:
- src/Dapplo.Jira/ServerDomainExtensions.cs
```

---

## Feature Request Templates

### Template: High Priority Missing Endpoint

**Title:** Add support for [Feature Name] API

**Description:**
The Dapplo.Jira library currently does not support the [Feature Name] API endpoint which is available in the JIRA Cloud/Server REST API.

**Use Case:**
[Describe why this feature is needed and how it would be used]

**Atlassian Documentation:**
[Link to Atlassian API documentation]

**Proposed Implementation:**
- Add methods to `[DomainExtensions.cs]` file
- Add entity classes to `src/Dapplo.Jira/Entities/` if needed
- Follow existing patterns in the library

**Endpoints to Implement:**
- `GET /rest/api/3/[endpoint]` - [Description]
- `POST /rest/api/3/[endpoint]` - [Description]
- `PUT /rest/api/3/[endpoint]` - [Description]
- `DELETE /rest/api/3/[endpoint]` - [Description]

**Example Usage:**
```csharp
var client = JiraClient.Create(uri);
var result = await client.[Domain].[MethodName](...);
```

**Additional Context:**
[Any other relevant information]

### Template: Deprecation Notice

**Title:** Update deprecated [Feature Name] API endpoint

**Description:**
The current implementation uses the deprecated `[endpoint]` which was deprecated by Atlassian on [date].

**Current Implementation:**
- File: `[filename]`
- Endpoint: `[deprecated endpoint]`

**Atlassian Deprecation Notice:**
[Link to deprecation notice]

**Proposed Changes:**
1. Migrate to new endpoint: `[new endpoint]`
2. Update method signatures if needed
3. Add deprecation warnings to old methods
4. Update documentation

**Breaking Changes:**
[Yes/No - describe any breaking changes]

**Migration Path:**
[Describe how users should migrate their code]

### Template: Missing Fields

**Title:** Add missing fields to [Entity Name]

**Description:**
The `[Entity Name]` entity is missing several fields that are available in the JIRA API response.

**Current File:** `src/Dapplo.Jira/Entities/[Entity].cs`

**Missing Fields:**
- `[fieldName]` - [type] - [description]
- `[fieldName]` - [type] - [description]

**Atlassian Documentation:**
[Link to field documentation]

**Impact:**
Users cannot access these fields even though they are returned by the API.

**Proposed Implementation:**
Add properties to the entity class following the existing pattern.

Example:
```csharp
[JsonProperty("fieldName")]
public Type FieldName { get; set; }
```

---

## Contributing

If you would like to contribute to implementing any of the missing features:

1. Check the [GitHub Issues](https://github.com/dapplo/Dapplo.Jira/issues) to see if someone is already working on it
2. Create a new issue using one of the templates above
3. Fork the repository and create a feature branch
4. Implement the feature following the existing code patterns
5. Add tests for your implementation
6. Submit a pull request

---

## References

### Atlassian JIRA Documentation
- [JIRA Cloud REST API Reference](https://developer.atlassian.com/cloud/jira/platform/rest/v3/)
- [JIRA Software Cloud REST API](https://developer.atlassian.com/cloud/jira/software/rest/)
- [JIRA Service Management API](https://developer.atlassian.com/cloud/jira/service-desk/rest/intro/)
- [API Changes and Deprecations](https://developer.atlassian.com/cloud/jira/platform/changelog/)

### Dapplo.Jira Documentation
- [Project Documentation](https://www.dapplo.net/Dapplo.Jira/index.html)
- [GitHub Repository](https://github.com/dapplo/Dapplo.Jira)
- [NuGet Package](https://www.nuget.org/packages/Dapplo.Jira)

---

**Last Updated:** 2026-01-29  
**Document Version:** 1.0  
**Library Version Analyzed:** Latest (main branch)
