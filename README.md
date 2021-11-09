# dfc-app-lmi-delta-report

## Introduction

This project provides a Delta report app used to report differences between consecutive LMI import as part of the LMI Quick wins project.

This web app is a stand alone MVC app, using Bootstrap and JQuery.

*This is not a Composite UI app*

## Getting started

This is a self-contained Visual Studio 2019 solution containing a number of projects (web application, service and repository layers, with associated unit test and integration test projects).

### Installing

Clone the project and open the solution in Visual Studio 2019.

## Local Config Files

Once you have cloned the public repo you need to create an app settings file from the configuration file listed below.

|Location|Filename|Rename to|
|--------|--------|---------|
|DFC.App.Lmi.Delta.Report|appsettings-template.json|appsettings.json|

## Configuring to run locally

The project contains *appsettings-template.json* files which contains LMI Delta Report appsettings for the web app. To use these files, copy them to *appsettings.json* within each project and edit and replace the configuration item values with values suitable for your environment.

## Running locally

To run this product locally, you will need to configure the LMI Delta Report API base address to point to the LMI Quick wins Delta Report API which is available from [https://github.com/SkillsFundingAgency/dfc-api-lmi-delta-report](https://github.com/SkillsFundingAgency/dfc-api-lmi-delta-report)

## Deployments

This LMI Delta report app will be deployed as an stand alone web app.

## Assets

CSS, JS, images and fonts used in this site may be found within the project itself, or as part of MVC, Bootstrap, JQuery.

## Built with

* Microsoft Visual Studio 2019
* .Net Core 3.1
