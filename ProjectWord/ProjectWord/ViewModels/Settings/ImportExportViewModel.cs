﻿#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.ToastMessages;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OpenDictionary.ViewModels
{
    public class ImportExportViewModel : ViewModel
    {
        private static readonly Color red = Color.FromHex("#dc3545");
        private static readonly Color white = Color.FromHex("#fff");
        private static readonly Color green = Color.FromHex("#198754");

        private readonly IStorage<WordGroup> storage;
        private readonly IToastMessageService toast;

        public AsyncCommand<ShareOptions> ShareCommand { get; }
        public AsyncCommand ImportCommand { get; }

        public ImportExportViewModel(IStorage<WordGroup> storage, IToastMessageService toast)
        {
            this.storage = storage;
            this.toast = toast;

            ShareCommand = new AsyncCommand<ShareOptions>(OnShare);

            ImportCommand = new AsyncCommand(OnImport);
        }

        private async Task OnImport()
        {
            FileResult picked = await FilePicker.PickAsync();

            if (picked is null)
            {
                return;
            }

            if (picked.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase) is false)
            {
                await ErrorMessage(message: "Please select a json file");

                return;
            }

            bool added = await ShareAsJson(picked.FullPath);

            if (added)
            {
                await SuccessMessage(message: "Successfull");
            }
            else
            {
                await ErrorMessage(message: "Unsuccessfull");
            }
        }
        private async Task<bool> ShareAsJson(string path)
        {
            StreamReader reader = new StreamReader(path);

            string json = await reader.ReadToEndAsync();

            WordGroup[]? groups = JsonConvert.DeserializeObject<WordGroup[]>(json);

            if (groups is null)
            {
                await ErrorMessage(message: "Json file have incorrect format");

                return false;
            }
            if (groups.Length is 0)
            {
                await ErrorMessage(message: "Dictionaries are empty");

                return false;
            }

            return await storage.AddRangeAsync(groups);
        }

        private async Task OnShare(ShareOptions share)
        {
            var groups = await storage.Query().Select(group => new
            {
                Name = group.Name,
                Words = group.Words.Select(word => new
                {
                    Origin = word.Origin,
                    Translation = word.Translation
                })
            }).ToArrayAsync();

            string json = JsonConvert.SerializeObject(groups, Formatting.Indented);

            if (share is ShareOptions.JsonFile)
            {
                string path = Path.Combine(FileSystem.CacheDirectory, "WordGroupArray.json");

                await WriteJsonFile(path, json);

                ShareFileRequest request = new ShareFileRequest
                {
                    Title = "Dictionaries",
                    File = new ShareFile(path)
                };

                await Share.RequestAsync(request);
            }
        }
        private static async Task WriteJsonFile(string path, string json)
        {
            StreamWriter file = File.CreateText(path);

            await file.WriteAsync(json);

            await file.FlushAsync();

            await file.DisposeAsync();
        }

        private Task SuccessMessage(string message)
        {
            return toast.Show(message, background: green, foreground: white);
        }
        private Task ErrorMessage(string message)
        {

            return toast.Show(message, background: red, foreground: white);
        }
    }
}