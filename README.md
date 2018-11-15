Note, as of 11/15/18, I no longer need a custom build task for DeployMaster because Jan Goyvaerts, maker of [DeployMaster](https://www.deploymaster.com) very kindly added my feature suggestion to accept the version number on the command line.

Also, I've learned a lot about GitHub's Octokit API client library in the last 24 hours that answers many of my questions I had when I wrote this readme.

---

I've tinkered around with continuous deployment stuff for .NET desktop apps before with my [AzureDeploy](https://github.com/adamosoftware/AzureDeploy) project. That project built an installer and uploaded it to Azure blob storage every time the app version number increased. It also offered a client library for determining when a new installer version was available. I wanted to take another whack at this that would be easier to setup as well as add a step to create GitHub releases automatically as part of the release process.

I considered something like AppVeyor, which I'm using on some other things already, and I like it a lot. However, AppVeyor is not free for private repositories, and I don't think it supports [DeployMaster](https://www.deploymaster.com/), the installer product I use. Furthermore, I wanted to keep using my local build environment, not for any strong reason other than it feels simpler.

As it stands now, I have a working sample that builds my [SQL Model Merge](https://aosoftware.net/Project/SqlModelMerge) app and uploadds to blob storage. However, there are a few things I'm looking for help with:

- Ability to create GitHub releases for an app's related repositories. I've started this [here](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/DeployTasks/CreateGitHubRelease.cs), but it's not functioning.

- Find an innovative approach for scripting deployment tasks with my [class library](https://github.com/adamosoftware/Delivery/tree/master/Delivery.Library). XAML itself might work, as it is, at heart, a generic way to describe a wide variety of CLR objects. But I don't know how to align or bind XAML source with a certain namespace or whatever.

## Solution Components

- [Deliver.App](https://github.com/adamosoftware/Delivery/tree/master/Delivery.App) was my very first stab at a UI to be used as an External Tool from Visual Studio. I quickly put this aside to focus on the class library side of it, and it's unclear if this app has any future.

- [Deliver.Library](https://github.com/adamosoftware/Delivery/tree/master/Delivery.Library) is the class library portion where all the real work is done. I'll talk more about this below.

- [Sample](https://github.com/adamosoftware/Delivery/tree/master/Sample) is a console app for testing the class library. Currently it has a sample deployment for my SQL Model Merge app as mentioned above.

## Deliver.Library

- [Classes](https://github.com/adamosoftware/Delivery/tree/master/Delivery.Library/Classes) has some base classes, notably [ExeProcess](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/Classes/ExeProcess.cs) and [ApiCall](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/Classes/ApiCall.cs) that form the basis of more specific deployment tasks you'll see in [DeployTasks](https://github.com/adamosoftware/Delivery/tree/master/Delivery.Library/DeployTasks).

  - [DeployManager](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/Classes/DeployManager.cs) is what runs all the deployment tasks for a solution. See example usage [here](https://github.com/adamosoftware/Delivery/blob/master/Sample/Program.cs#L21).
  
  - [TaskCredentials](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/Classes/TaskCredentials.cs) is what I use for storing service credentials locally to ensure that I don't commit sensitive info to source control. It works like a database connection string of name-value pairs, except it uses colons instead of equal signs. (This is because the equal sign appears in base64 strings, used by Azure blob storage account keys.) I use my [JsonSettings](https://github.com/adamosoftware/JsonSettings) project to help here. Sample use [here](https://github.com/adamosoftware/Delivery/blob/master/Sample/Program.cs#L44). See also [here](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/Classes/DeployManager.cs#L47) where they are referenced by DeployManager. See [here](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/DeployTasks/UploadToBlobStorage.cs#L57) to see how blob storage account info is set.

- [DeployTasks](https://github.com/adamosoftware/Delivery/tree/master/Delivery.Library/DeployTasks) has the three deployment tasks I need for my situation. Two are working, [BuildDeployMaster](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/DeployTasks/BuildDeployMaster.cs) and [UploadToBlobStorage](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/DeployTasks/UploadToBlobStorage.cs). Both implement [IDeployTask](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/Interfaces/IDeployTask.cs). [CreateGitHubRelease](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/DeployTasks/CreateGitHubRelease.cs) is what I need help with, mainly because I'm not sure how present login UI or if there's a way to bypass with token or even use my [TaskCredentials](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/Classes/TaskCredentials.cs) class.

- [Interfaces](https://github.com/adamosoftware/Delivery/tree/master/Delivery.Library/Interfaces) has only one interface used in the project [IDeployTask](https://github.com/adamosoftware/Delivery/blob/master/Delivery.Library/Interfaces/IDeployTask.cs)
