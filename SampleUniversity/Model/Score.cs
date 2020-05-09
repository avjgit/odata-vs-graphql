using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace SampleUniversity.Model
{
    public class FavReps
    {
        [JsonPropertyName("score")]
        public Repository FavRep
        {
            get => GitHubODataClient.GetRepositoryInfo("andrejs").Result.Items.FirstOrDefault(); 
            set {}
        }
    }
}
