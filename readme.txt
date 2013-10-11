10/11/2013

No changes. Putting all source into a GIT repo.

8/09/2011

Here's a more fully working version of TextMap.  Includes Save and Open, so you can save your work and go back and edit it later.  The "Copy to Clipboard" menu item is gone, and replaced by just "Copy".  Cut, copy and paste work within the app and without, so you can copy from all or any portion of the TextMap and paste to all or any portion of the TextMap.  For example you could copy the entire map and paste it into Notepad. Then you can copy it from Notepad and paste it back into TextMap.

Selection is now "sticky".  When you drag out a selection rectangle it stays selected until you press the mouse left button again.  This allows multiple operations on one selection.  A good use for this is the Rotate function (Ctrl-R), which rotates right 90 degrees.  If you want to rotate 180 degrees just press Ctrl-R twice in a row.

When pasting to a selection rectangle, text will be truncated if needed.  In general the program tries to be smart about pasting and aggressively truncates to avoid any invalid paste.    A paste with no current selection will attempt to paste the entire block of copied text at the current grid cell, using the current cell as the top, left corner of the region.  Again, this will be truncated if text would be pasted outside the map region.

Rotating a square region should work as you would expect.  You can also rotate a non-square region but the results may be non-intuitive due to truncation-as-needed.

You can now change the grid size of the map via the Properties dialog.  The default is 80,25.  If you change the grid size and save a map the map size will be restored when re-opening the same map file later.  By default maps are saved as *.textmap files, but you can change the extension to whatever you want.

Some other features I could imagine adding:
- recently used file list
- undo/redo
- auto-save of programs settings like grid size
- move or drag/drop of selections

I hope this will make creating your Heretic map easier.   Let me know how well works for you and any ideas you might have about making it better.   Bug reports, feature requests and comments are welcomed.
