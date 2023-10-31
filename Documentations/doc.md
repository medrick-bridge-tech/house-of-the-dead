# Restrictions

- Vertical slice should not be more than 100mb

## Optimization for main character
Optimized the main character model, using decimation in blender to enhance performance while maintaining visual quality.

Results:

- Original triangle count: 12,700
- Optimized triangle count: 5,600 (55.90% reduction)

## Camera system

The camera switching system works with a priority-based approach.
  
  ```csharp
  private const int puzzleSelectPriority = 3, puzzleDeselectPriority = -1;
  private const int roomSelectPriority = 2, roomDeselctPriority = 1;
  ```
  
so:
  - **For Room Cameras:** the priority for any new room camera should be set to 1, unless it's the starting room that should be set to 2.
  - **For Puzzle Camera:** the priority for any new puzzle camera should be set to -1.
    
  
  
