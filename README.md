# Snowflake Data

A Simple Dotnet Application Demonstrating Connecting and Querying a Snowflake Database

In order to build the project locally, please install the .NET Core SDK.

Due to compatibility testing  we choose to target an older version of the .NET Core Buildpack (v 2.3.3).  Since the project was built with a much newer version of the .NET SDK you'll have to do an artifact push and NOT a source code push.  To complete this from the .NET CLI run:

```c#
    dotnet publish -o  .\publish
```

And then target your output folder with your path variable during your `cf push` or in your manifest.yml

## Expected Results

A UI that on it's home page displays the connection information and a list of suppliers queried from the test supplier database generated when you create a Snowflake account.

![Application](/images/Application.png)

The Data seen on the home screen of the app should match the data in the Snowflake Console.

![Snowflake](/images/Snowflake.png)

If you'd like to connect this repository to your own database edit the HomeController and the hardcoded values for the respective connection parameters.  If you'd like to utilize my connection parameters please reach out to me.

![Parameters](/images/Parameters.png)
