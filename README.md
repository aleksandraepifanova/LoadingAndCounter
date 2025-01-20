# README

## Overview

This project is a small interactive "game" that demonstrates:

- Loading JSON files and an Asset Bundle on startup.
- Displaying and updating content dynamically using Unity.
- Managing Asset Bundles and JSON data effectively.

The application consists of two main scenes:

1. **Loading Scene**: Loads necessary data and transitions to the main screen.
2. **Main Scene**: Displays a welcome message, a counter, and supports content updates through a button.

## Features

1. **Loading Scene**:

   - Displays a loading progress bar and text.
   - Loads:
     - A JSON file for settings (`Settings.json`).
     - A JSON file for the welcome message (`Message.json`).
     - An Asset Bundle containing a sprite.
   - Transitions to the main scene upon successful loading.

2. **Main Scene**:

   - Displays a welcome message and a counter.
   - Features two buttons:
     - **"Increase Counter"**: Increments the counter and updates its display.
     - **"Update Content"**: Reloads the JSON files and Asset Bundle, updating the displayed content dynamically.

3. **Persistent Data**:

   - Saves the counter value to persistent storage and restores it on app restart.
   - Uses the `startingNumber` from `Settings.json` if no previous data exists.

## File Structure

```
Assets/
|-- Scenes/
|   |-- LoadingScene.unity
|   |-- MainScene.unity
|-- Scripts/
|   |-- LoadingManager.cs
|   |-- MainSceneManager.cs
|-- StreamingAssets/
|   |-- Settings.json
|   |-- Message.json
|   |-- example.bundle
```

## JSON File Examples

### `Settings.json`

```json
{
  "startingNumber": 5
}
```

### `Message.json`

```json
{
  "welcomeMessage": "Welcome to the application!"
}
```

## Asset Bundle

The Asset Bundle (`example.bundle`) contains:

- A sprite with the name `ExampleSprite` used as the button background.

