﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsOverLan
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public sealed class GameSettings
	{
		private const string DefaultServerName = "Cards Over LAN";
		private const int DefaultHandSize = 10;
		private const int DefaultMinPlayers = 3;
		private const int DefaultMaxPlayers = 10;
		private const int DefaultMaxSpectators = 10;
		private const int MinMaxPlayers = 3;
		private const int MinMinPlayers = 3;
		private const int MinMaxPoints = 1;
		private const int MinRoundEndTimeout = 0;
		private const int MinGameEndTimeout = 10000;
		private const int MinAfkTimeSeconds = 30;
		private const int MinAfkRecoveryTimeSeconds = 30;
		private const int MinBlankCards = 0;
		private const int MinBotCount = 0;
		private const int MinDiscards = 0;

		private const int DefaultRoundEndTimeout = 10000;
		private const int DefaultGameEndTimeout = 30000;
		private const int DefaultAfkTimeSeconds = 300;
		private const int DefaultAfkRecoveryTimeSeconds = 90;
		private const int DefaultMaxPoints = 10;
		private const int DefaultMaxRounds = 16;
		private const int DefaultBlankCards = 0;
		private const int DefaultBotCount = 0;
		private const int DefaultDiscards = 5;

		private int _blankCards = DefaultBlankCards;
		private int _maxPoints = DefaultMaxPoints;
		private int _maxPlayers = DefaultMaxPlayers, _maxNameLength;
		private int _handSize = DefaultHandSize;
		private int _minPlayers = DefaultMinPlayers;
		private int _roundEndTimeout = DefaultRoundEndTimeout;
		private int _gameEndTimeout = DefaultGameEndTimeout;
		private int _discards = DefaultDiscards;

		private int _afkTimeSeconds = DefaultAfkTimeSeconds;
		private int _afkRecoveryTimeSeconds = DefaultAfkRecoveryTimeSeconds;
		private int _botCount;
		private int _maxSpectators;

		[JsonProperty("server_name", DefaultValueHandling = DefaultValueHandling.Ignore, Required = Required.DisallowNull)]
		[DefaultValue(DefaultServerName)]
		public string ServerName { get; set; } = DefaultServerName;

		[JsonProperty("host", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue("http://localhost:80")]
		public string Host { get; set; }

		[JsonProperty("min_players", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultMinPlayers)]
		public int MinPlayers
		{
			get => _minPlayers;
			set => _minPlayers = value < MinMinPlayers ? MinMinPlayers : value;
		}

		[JsonProperty("max_players", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultMaxPlayers)]
		public int MaxPlayers
		{
			get => _maxPlayers;
			set => _maxPlayers = value < MinMaxPlayers ? MinMaxPlayers : value;
		}

		[JsonProperty("max_spectators", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultMaxSpectators)]
		public int MaxSpectators
		{
			get => _maxSpectators;
			set => _maxSpectators = value < 0 ? 0 : value;
		}

		[JsonProperty("max_player_name_length", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(48)]
		public int MaxPlayerNameLength
		{
			get => _maxNameLength;
			set => _maxNameLength = value <= 0 ? 1 : value;
		}

		[JsonProperty("allow_duplicates", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(false)]
		public bool AllowDuplicatePlayers { get; set; } = false;

		[JsonProperty("hand_size", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(10)]
		public int HandSize
		{
			get => _handSize;
			set => _handSize = value <= 4 ? 4 : value;
		}

		[JsonProperty("blank_cards", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultBlankCards)]
		public int BlankCards
		{
			get => _blankCards;
			set => _blankCards = value <= MinBlankCards ? MinBlankCards : value;
		}

		[JsonProperty("round_end_timeout", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultRoundEndTimeout)]
		public int RoundEndTimeout
		{
			get => _roundEndTimeout;
			set => _roundEndTimeout = value < MinRoundEndTimeout ? MinRoundEndTimeout : value;
		}

		[JsonProperty("game_end_timeout", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultGameEndTimeout)]
		public int GameEndTimeout
		{
			get => _gameEndTimeout;
			set => _gameEndTimeout = value < MinGameEndTimeout ? MinGameEndTimeout : value;
		}

		[JsonProperty("afk_time_seconds", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultAfkTimeSeconds)]
		public int AfkTimeSeconds
		{
			get => _afkTimeSeconds;
			set => _afkTimeSeconds = value < MinAfkTimeSeconds ? MinAfkTimeSeconds : value;
		}

		[JsonProperty("afk_recovery_time_seconds", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultAfkRecoveryTimeSeconds)]
		public int AfkRecoveryTimeSeconds
		{
			get => _afkRecoveryTimeSeconds;
			set => _afkRecoveryTimeSeconds = value < MinAfkRecoveryTimeSeconds ? MinAfkRecoveryTimeSeconds : value;
		}

		[JsonProperty("perma_czar", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(false)]
		public bool PermanentCzar { get; set; }

		[JsonProperty("bot_czars", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(true)]
		public bool AllowBotCzars { get; set; } = true;

		[JsonProperty("winner_czar", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(false)]
		public bool WinnerCzar { get; set; } = false;

		[JsonProperty("exclude_content", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string[] ContentExclusions { get; set; } = new string[0];

		[JsonProperty("max_points", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultMaxPoints)]
		public int MaxPoints
		{
			get => _maxPoints;
			set
			{
				_maxPoints = value < MinMaxPoints ? MinMaxPoints : value;
			}
		}

		[JsonProperty("max_rounds", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultMaxRounds)]
		public int MaxRounds { get; set; }

		[JsonProperty("bot_count", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultBotCount)]
		public int BotCount
		{
			get => _botCount;
			set => _botCount = value < MinBotCount ? MinBotCount : value;
		}

		[JsonProperty("bot_names", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string[] BotNames { get; set; } = new string[0];

		[JsonProperty("require_languages", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string[] RequiredLanguages { get; set; } = new string[0];

		[JsonProperty("use_packs", Required = Required.DisallowNull)]
		public string[] UsePacks { get; set; } = new string[0];

		[JsonProperty("exclude_packs", Required = Required.DisallowNull)]
		public string[] ExcludePacks { get; set; } = new string[0];

		[JsonProperty("enable_upgrades", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(true)]
		public bool UpgradesEnabled { get; set; } = true;

		[JsonProperty("discards", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(DefaultDiscards)]
		public int Discards
		{
			get => _discards;
			set => _discards = value < MinDiscards ? MinDiscards : value;
		}

		[JsonProperty("allow_skips", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(true)]
		public bool AllowBlackCardSkips { get; set; } = true;

		[JsonProperty("pick_one_only", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(false)]
		public bool PickOneCardsOnly { get; set; } = false;

		[JsonProperty("enable_chat", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(true)]
		public bool ChatEnabled { get; set; } = true;

		[JsonProperty("enable_bot_taunts", DefaultValueHandling = DefaultValueHandling.Populate)]
		[DefaultValue(true)]
		public bool BotTauntsEnabled { get; set; } = true;

		public static GameSettings FromFile(string path) => JsonConvert.DeserializeObject<GameSettings>(File.ReadAllText(path));
	}
}
