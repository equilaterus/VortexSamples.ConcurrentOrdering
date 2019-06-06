# VortexSamples.ConcurrentOrdering

This sample shows how to use Vortex and how you can still use every known concept of ASP Core, like handling *Concurrency Conflicts*. This project also showcases a recommended project structure for small to medium sized solutions.

## Features

* [Controllers](https://github.com/equilaterus/VortexSamples.ConcurrentOrdering/tree/master/src/ConcurrentOrdering.Web/Controllers/Api) built with **Vortex** monads.

* Use of [built-in ASP Core DI](https://github.com/equilaterus/VortexSamples.ConcurrentOrdering/blob/master/src/ConcurrentOrdering.Web/Startup.cs) to inject *side-effects* (Ef Core repositories and Automapper) on a single entry-point (controllers).

* Injecting [behaviors](https://github.com/equilaterus/VortexSamples.ConcurrentOrdering/blob/master/src/ConcurrentOrdering.Domain/Behaviors/OrderBehavior.cs) (*not dependencies*) using **Vortex**.

* EfCore [Concurrency Checks](https://github.com/equilaterus/VortexSamples.ConcurrentOrdering/blob/master/src/ConcurrentOrdering.Domain/Models/Product.cs) integrated with a [global error handling](https://github.com/equilaterus/VortexSamples.ConcurrentOrdering/blob/master/src/ConcurrentOrdering.Web/Controllers/HomeController.cs). *Note*: Concurrency expection errors return a flag *AllowRetry* so your client app can show better feedback to the user or even automatically retry operations.

* Sample [tests](https://github.com/equilaterus/VortexSamples.ConcurrentOrdering/tree/master/test/ConcurrentOrdering.Domain.Tests).

* MVC App using Javascript and jQuery.

## Project Structure

* **ConcurrentOrdering.Domain**: This project contains all classes related with business logic and persistance of the application.

  * **Behaviors**: Those are *static classes* that contain the *business logic*. Each method should be a [Pure Function](https://en.wikipedia.org/wiki/Pure_function), that give us guarantees on the behavior of the Application's core but also makes it easier to test.

  * **Infrastructure**: Data-persistance and other side effects.
  
    * **Data**: Data-contexts and data-seeds.
	
	* **Repositories**: Persistance-related classes.

  * **Models**: Domain anemic entities.

* **ConcurrentOrdering.Domain.Tests**: Sample tests (this doesn't intend to be a complete test suite for a production project).

* **ConcurrentOrdering.Web**: This project contains all specific *web-app* stuff.

  * **wwwroot**: javascript and css.

  * **Commands**: Actions that can be executed by the controllers.

  * **Controllers**: They contain **Vortex Monands** that handle the execution of an action received by the web-app.

  * **Extensions**: Extended methods.

  * **Migrations**: EfCore migrations.

  * **Models**: You can add here View Models and other kind of DTOs.

  * **Views**: Razor views.

