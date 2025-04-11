#nullable disable
using System;

namespace SnakeApi.Models.DTOs
{
    public class ScoreDTO
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public int ScoreValue { get; set; }
    }
}
