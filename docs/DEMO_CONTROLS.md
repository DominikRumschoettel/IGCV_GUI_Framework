# IGCV GUI Framework - Demo Controls Guide

This guide provides an overview of the control demonstrations included in the IGCV GUI Framework. The demonstration application showcases the framework's themed controls and layout capabilities across multiple specialized pages.

## Related Documentation
- [README](../README.md) - Main project documentation and quick start
- [User Guide](USER_GUIDE.md) - Practical guide for developers using the framework
- [Architecture Overview](ARCHITECTURE.md) - Technical details of the framework architecture
- [LLM Developer Guide](LLM_DEVELOPER_GUIDE.md) - Detailed guide for Language Model-based development

## Demo Pages Overview

The demonstration application is organized into specialized pages, each focusing on a specific category of controls:

1. **Button Controls** - Demonstrates various button styles and usage patterns
2. **Input Controls** - Showcases text inputs, dropdowns, checkboxes, and radio buttons
3. **Progress Controls** - Displays progress indicators including progress bars and step indicators
4. **Layout Controls** - Demonstrates panel layouts, cards, grids, and dashboard components

## Button Controls Page

The Button Controls page demonstrates all available button variants and their usage patterns.

### Button Variants

* **Primary Buttons** - The main action buttons with high emphasis
* **Secondary Buttons** - Alternative or secondary action buttons
* **Tertiary Buttons** - Text-style buttons for less important actions
* **Specialized Buttons** - Success, warning, danger, and custom colored buttons

### Button Features

* Different sizes (small, regular, large)
* Disabled state
* Icon integration
* Custom colors 
* Rounded corners
* Mouse hover and pressed states

### Usage Examples

* Form submission pattern
* Confirmation dialog pattern
* Action button groups

## Input Controls Page

The Input Controls page showcases various input controls for collecting user data.

### Input Control Variants

* **Text Inputs** - Regular, password, disabled, and validated text boxes
* **Dropdown Controls** - Regular dropdown, dropdown list, and specialized selectors
* **Selection Controls** - Checkboxes, radio buttons, and grouped selection controls

### Input Features

* Placeholder text
* Validation feedback
* Grouping of related inputs
* Focus states
* Disabled states

### Usage Examples

* Search pattern
* Filter pattern
* Settings configuration

## Progress Controls Page

The Progress Controls page demonstrates various ways to show progress and steps.

### Progress Indicators

* **Progress Bars** - Different states (0%, 25%, 50%, 75%, 100%)
* **Step Indicators** - Multi-step processes with completed, current, and future states

### Interactive Demonstrations

* File upload simulation with live progress updates
* Multi-step form navigation
* Task completion tracking

## Layout Controls Page

The Layout Controls page showcases layout patterns and containers.

### Panel Variants

* Standard panels
* Panels with rounded corners
* Panels with borders
* Colored panels

### Layout Patterns

* **Card Layout** - Content cards with headers and content
* **Grid Layout** - Organized grid of content cells
* **Dashboard Layout** - Complete dashboard with multiple components

### Dashboard Example

* KPI (Key Performance Indicator) cards
* Chart panel
* Statistics panel

## Using the Demo in Your Development

The demos serve as practical examples of how to implement the controls in your own applications. You can:

1. **Study the Implementation** - Examine the source code for each demo page
2. **Copy Patterns** - Reuse layout and control patterns in your applications
3. **Experiment** - Modify the demos to test different styling options

## Extending the Demos

You can extend the demo application by:

1. **Adding New Pages** - Create new specialized pages to showcase additional controls
2. **Creating New Patterns** - Implement new UI patterns and add them to existing pages
3. **Integrating with Data** - Connect the demos to real data sources

## Running the Demo

To run the demonstration application:

1. Build the solution in Visual Studio:
   ```
   dotnet build
   ```

2. Run the IGCV_GUI_Framework project:
   ```
   dotnet run --project IGCV_GUI_Framework/IGCV_GUI_Framework.csproj
   ```

This will launch the application window (1200x800 pixels) with the control demonstrations.

## Customizing the Demo Application

If you want to customize the demo application:

1. Modify the window size constants in `main-form.cs`:
   ```csharp
   private const int WINDOW_WIDTH = 1200;
   private const int WINDOW_HEIGHT = 800;
   ```

2. Add or remove pages in the `InitializePages` method:
   ```csharp
   _pages.Add(new YourCustomDemoPage());
   ```

3. Adjust control positions and sizes in each page's `InitializeComponent` method to suit your needs

For more detailed information on implementation, refer to the [LLM Developer Guide](LLM_DEVELOPER_GUIDE.md).
