# Project created from

Install-Package RabbitMQ.Client

Install-Package MongoDB.Driver

App.config
==========
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6.1" />
    </startup>
   <appSettings>
      <add key="usernamequeue" value="usernamequeue" />
      <add key="passwordqueue" value="passwordqueue" />
      <add key="addressqueue" value="localhost" />
      <add key="portqueue" value="5672" />
	  <add key="usernamedb" value="usernamedb" />
	  <add key="passworddb" value="passworddb" />
	  <add key="portdb" value="portdb" />
	  <add key="dbname" value="dbname" />
   </appSettings>
</configuration>