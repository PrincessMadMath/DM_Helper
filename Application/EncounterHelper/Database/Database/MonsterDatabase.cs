﻿using System;
using System.Collections.Generic;
using Database.Factory;
using EncounterHelper;
using Helper;

namespace Database.Database
{
    public static class MonsterDatabase
    {
        private static readonly Dictionary<string, MonsterBuilder> Repository = new Dictionary<string, MonsterBuilder>();

        public static void Load(string directoryPath)
        {
            var filePaths = FileHelper.GetAllFilesInDirectoryAndSubDirectories(directoryPath, "*_m.json");
            foreach (var filePath in filePaths)
            {
                var serializeString = FileHelper.ReadFile(filePath);
                var monster = JsonParserHelper.ParseSingleton<MonsterBuilder>(serializeString);
                if (monster != null)
                {
                    if (monster.MonsterType == null)
                    {
                        Console.WriteLine("A monster stat has no race");
                        continue;
                    }
                    Repository.Add(monster.MonsterType, monster);
                }

            }
        }

        public static MonsterBuilder GetMonsterStat(string monsterType)
        {
            return Repository[monsterType];
        }
    }
}