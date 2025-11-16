//
// VersionInfo.cs
//
// Author(s):
//       Josh Montoute <josh@thinksquirrel.com>
//
// Copyright (c) 2012-2015 Thinksquirrel Software, LLC
//
using System.Text;

namespace Thinksquirrel.CShake.Internal
{
    /// <summary>
    /// Contains version information for Camera Shake.
    /// </summary>
    static class VersionInfo
    {
        const int _major = 1;
        const int _minor = 5;
        const int _incremental = 0;
        const int _revision = 1;
        const string _revisionString = "f";
        const bool _isPreRelease = false;
        
        /// <summary>
        /// Gets the incremental version number.
        /// </summary>
        public static int incremental
        {
            get
            {
                return _incremental;
            }
        }

        /// <summary>
        /// Gets the major version number.
        /// </summary>
        public static int major
        {
            get
            {
                return _major;
            }
        }

        /// <summary>
        /// Gets the minor version number.
        /// </summary>
        public static int minor
        {
            get
            {
                return _minor;
            }
        }

        /// <summary>
        /// Gets the revision version number.
        /// </summary>
        public static int revision
        {
            get
            {
                return _revision;
            }
        }

        /// <summary>
        /// Whether or not the current Camera Shake build is a beta.
        /// </summary>
        public static bool isPreRelease
        {
            get
            {
                return _isPreRelease;
            }
        }

        /// <summary>
        /// Gets the version of Camera Shake.
        /// </summary>
        public static string version
        {
            get
            {
                return string.Format("{0}.{1}.{2}{3}{4}", _major, _minor, _incremental, _revisionString, _revision);
            }
        }

        /// <summary>
        /// Gets the current Camera Shake license, in human-readable form.
        /// </summary>
        public static string license
        {
            get
            {
                return "Camera Shake";
            }
        }

        /// <summary>
        /// Gets the copyright message.
        /// </summary>
        public static string copyright
        {
            get
            {
                return "(c) 2012-2015 Thinksquirrel Software, LLC";
            }
        }
    }
}
