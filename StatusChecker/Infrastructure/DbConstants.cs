﻿using System;
using System.IO;

public static class DbConstants
{
    /// <summary>
    /// Filename for SQLite Database
    /// </summary>
    public const string DatabaseFilename = "StatusChecker.db";

    public const SQLite.SQLiteOpenFlags Flags =

        SQLite.SQLiteOpenFlags.ReadWrite |

        SQLite.SQLiteOpenFlags.Create |

        SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath
    {
        get
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, DatabaseFilename);
        }
    }
}