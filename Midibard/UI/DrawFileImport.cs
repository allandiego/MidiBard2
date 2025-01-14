﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Dalamud.Game.ClientState.Keys;
using Dalamud.Interface;
using Dalamud.Interface.ImGuiFileDialog;
using Dalamud.Logging;
using ImGuiNET;
using Microsoft.Win32;
using MidiBard.DalamudApi;
using MidiBard.Managers.Ipc;
using MidiBard2.Resources;
using MidiBard.UI.Win32;
using MidiBard.Util;

namespace MidiBard;

public partial class PluginUI
{
    #region import
    private void RunImportFileTask()
    {
        if (!IsImportRunning)
        {
            IsImportRunning = true;

            if (MidiBard.config.useLegacyFileDialog)
            {
                RunImportFileTaskWin32();
            }
            else
            {
                RunImportFileTaskImGui();
            }
        }
    }

    private void RunImportFolderTask()
    {
        if (!IsImportRunning)
        {
            IsImportRunning = true;

            if (MidiBard.config.useLegacyFileDialog)
            {
                RunImportFolderTaskWin32();
            }
            else
            {
                RunImportFolderTaskImGui();
            }
        }
    }


    private void RunImportFileTaskWin32()
    {
        FileDialogs.OpenMidiFileDialog((result, filePaths) =>
        {
	        if (result == true)
	        {
		        Task.Run(async () =>
		        {
			        try
			        {
				        await PlaylistManager.AddAsync(filePaths);
			        }
			        finally
			        {
				        IsImportRunning = false;
			        }
		        });
	        }
	        else
	        {
		        IsImportRunning = false;
	        }
        });
    }

    private void RunImportFileTaskImGui()
    {
        fileDialogManager.OpenFileDialog("Open", ".mid,.midi,.mmsong", (b, strings) =>
        {
            PluginLog.Debug($"dialog result: {b}\n{string.Join("\n", strings)}");
            if (b)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        await PlaylistManager.AddAsync(strings.ToArray());
                    }
                    finally
                    {
                        IsImportRunning = false;
                    }
                });
            }
            else
            {
                IsImportRunning = false;
            }
        }, 0);
    }

    private void RunImportFolderTaskImGui()
    {
        fileDialogManager.OpenFolderDialog("Open folder", (b, filePath) =>
        {
            PluginLog.Debug($"dialog result: {b}\n{string.Join("\n", filePath)}");
            if (b)
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var files = Directory.GetFiles(filePath, "*.mid", SearchOption.AllDirectories);
                        await PlaylistManager.AddAsync(files);
                        files = Directory.GetFiles(filePath, "*.midi", SearchOption.AllDirectories);
                        await PlaylistManager.AddAsync(files);
                        files = Directory.GetFiles(filePath, "*.mmsong", SearchOption.AllDirectories);
                        await PlaylistManager.AddAsync(files);
                    }
                    finally
                    {
                        IsImportRunning = false;                        
                    }
                });
            }
            else
            {
                IsImportRunning = false;
            }
        });
    }

    private void RunImportFolderTaskWin32()
    {
        FileDialogs.FolderPicker((result, folderPath) =>
        {
            if (result == true)
            {
                Task.Run(async () =>
                {
                    if (Directory.Exists(folderPath))
                    {
                        try
                        {
                            var files = Directory.GetFiles(folderPath, "*.mid", SearchOption.AllDirectories);
                            await PlaylistManager.AddAsync(files);
                            files = Directory.GetFiles(folderPath, "*.midi", SearchOption.AllDirectories);
                            await PlaylistManager.AddAsync(files);
                            files = Directory.GetFiles(folderPath, "*.mmsong", SearchOption.AllDirectories);
                            await PlaylistManager.AddAsync(files);
                        }
                        finally
                        {
                            IsImportRunning = false;
                        }
                    }
                });
            }
            else
            {
                IsImportRunning = false;
            }
        });
    }
    
    public bool IsImportRunning { get; private set; }
    
    #endregion
}