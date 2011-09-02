/*
 * Checksum - Verify the integrity of files.
 * Copyright (C) 2011 John Bird <https://github.com/jbird/Checksum>
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Collections;

namespace Checksum {
    /**
     * <summary>A file integrity verifier.</summary>
     */
    internal static class Checksum {
        /**
         * <summary>Cryptographic Hash Functions</summary>
         */
        public enum Hash {
            MD5 = 0,
            SHA1 = 1,
            SHA256 = 2,
            SHA512 = 3
        }

        public static IEnumerable HashAlgorithms() {
            return new Dictionary<int, string>() {
                {0, "MD5" }, {1, "SHA-1" }, {2, "SHA-256" }, {3, "SHA-512" }
            };
        }

        public static string ComputeChecksum(Hash hash, string path) {
            switch(hash) {
                case Hash.MD5:
                    return ComputeMD5Checksum(path);
                case Hash.SHA1:
                    return ComputeSHA1Checksum(path);
                case Hash.SHA256:
                    return ComputeSHA256Checksum(path);
                case Hash.SHA512:
                    return ComputeSHA512Checksum(path);
                default:
                    return String.Empty;
            }
        }

        private static string ComputeMD5Checksum(string path) {
            try {
                using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                    using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider()) {
                        byte[] hash = md5.ComputeHash(fs);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                } 
            } catch(Exception ex) { throw ex; }
        }

        private static string ComputeSHA1Checksum(string path) {
            try {
                using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                    using(SHA1Managed sha1 = new SHA1Managed()) {
                        byte[] hash = sha1.ComputeHash(fs);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            } catch(Exception ex) { throw ex; }
        }

        private static string ComputeSHA256Checksum(string path) {
            try {
                using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                    using(SHA256Managed sha256 = new SHA256Managed()) {
                        byte[] hash = sha256.ComputeHash(fs);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            } catch(Exception ex) { throw ex; }
        }

        private static string ComputeSHA512Checksum(string path) {
            try {
                using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                    using(SHA512Managed sha512 = new SHA512Managed()) {
                        byte[] hash = sha512.ComputeHash(fs);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            } catch(Exception ex) { throw ex; }
        }
    }
}
