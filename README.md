# Project Overview

This project is designed using the Zenject and UniTask plugins. It includes a basic UI implementation with four views: `MainView`, `UsersView`, `TodosView`, and `PhotosView`. Besides, it also includes popup implementation with `LoadingPopup` and `AlertPopup`. In addition, the project has been implemented from scratch Rest Client, which allows making REST API requests.

## Installation

Clone the repository and open the project in Unity. Make sure you have installed the Zenject and UniTask plugins.

## Usage

To use the project, open the Unity Editor and run the scene. From the `MainView`, you can navigate to other views like `UsersView`, `TodosView`, and `PhotosView`. The Rest Client can be configured using a "Rest Client Config" Scriptable object in the project. You can configure the following details:

- Origin URL
- Global Headers
- Environment

You can also configure different environments like Dev, Staging, and Production using the "Rest Client Config Factory".

## Views

### MainView

The `MainView` allows navigation between views.

### UsersView

The `UsersView` retrieves users from the server and shows them in a scroll list.

### TodosView

The `TodosView` retrieves todos from the server and shows them in a scroll list.

### PhotosView

The `PhotosView` retrieves photos from the server and starts downloading them to show in a scroll list with appropriate fields.

## Popups

### LoadingPopup

The `LoadingPopup` is shown when some process is going on.

### AlertPopup

The `AlertPopup` is shown when some process returns an exception.

## REST API Requests

The REST Client supports various REST API methods like GET and POST. The following are the available methods:

- **Basic Get**: Returns byte[] array of the response.
- **Typed Response Get**: Returns a response by deserialization of the concrete type.
- **Explicit Get**: Works with giving an absolute URL, but the same as the Basic Get.
- **Explicit Get Texture**: Returns the response as Texture2D.
- **Post**: Sends a request with the given payload and returns deserialized response.

## Author

This project was created by Arman Karapetyan.
