# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [3.3.1] - 2018-5-22

### Added
 - Add Ninject.Web.Common.cs in Ninject.Web.Common.WebHost package

## [3.3.0] - 2017-10-23

### Added
 - Support .NET Standard 2.0

### Removed
 - Dropped support for older .NET platforms (pre-.NET45)

### Changed
 - Move all System.Web related code from Ninject.Web.Common to Ninject.Web.Common.WebHost
 - Turn OwinBootstrapper into a MiddlewareFactory instead of a Delegate middleware

## [3.2.3]

### Added
- Owin 3.0 support

## [3.2.2]

### Fixed
- InRequestScoped objects were not released immediatly anymore due to missing call registration in GlobalKernelRegistration.

## [3.2.1]

### Added
- WebCommonNinjectModule that contains the bindings that are shared among all web extesnions to support multiple of them in one application. 

## [3.2.0.0]

### Added
- OWIN support.

## [3.0.0.0]
- Inintial release: This package is the base for all web extensions.
- Changed: OnePerRequestModule has been renamed to OnePerRequestHttpModule 
