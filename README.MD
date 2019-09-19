# CoreDevOpsGit

The project makes use of the libgit2 library in its version for .Net in a console application that, after reading a configuration file, shows the repositories available for download, hopes to obtain the user's option and finally executes the option chosen.

## Requirements

* [.Net Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)
* [LibGit2Sharp](https://github.com/libgit2/libgit2sharp)

## Steps to dev

* Download and install requirements
* Download the project
* Run in the Project directory: dotnet restore

## JSon File Settings

An example of the structure that the appsettings.json file should have is downloaded with the project

* At the root create an object named "config" with the following structure
  * EnterpriseName: The name of your company to display in the console
  * GitCredentials: JSon object with the following fields:
    * Username: The user with the necessary permissions in the git repository
    * Password: The password to access the git repository of the indicated user
  * PackageList: a vector of the repositories available for download from the utility. Each repository contains the following fields
    * Id: the "identifier" of the repository, each repository must have a different number, it is advisable to fill it in list mode.
    * Name: the name with which we will identify the repository in the console
    * PathDestination: The destination directory where we want to clone the repository
    * GroupName: We can group different repositories in packages, here we would give you the name with which we could identify them later.
    * Operations: A vector of operation type objects, are actions to be performed after cloning. In principle there is only copy.
      * Name: an identifying name, although we will not see it in console.
      * Command: In principle only copy: cp
      * Origin: the name of the file to copy
      * Destination: the new name with which we want the origin to stick
