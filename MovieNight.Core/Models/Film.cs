﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MovieNight.Core.Models
{
    public class Film
    {
        public bool adult { get; set; }
        private string Backdrop_path;
        public string backdrop_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Backdrop_path;
            }
            set
            {
                Backdrop_path = value;
            }
        }
        public object belongs_to_collection { get; set; }
        private int Budget;
        public string budget
        {
            get
            {
                return Budget.ToString("C0");
            }
            set
            {
                Budget = Int32.Parse(value);
            }
        }
        //public Genre[] genres { get; set; }
        public List<Genre> genres { private get; set; }
        public string Genres
        {
            get
            {
                string builder = "";

                foreach (Genre g in genres)
                {
                    builder += g.name + ", ";
                }

                if (builder.Length > 0)
                {
                    builder = builder.Substring(0, builder.Length - 2);
                }
                else
                {
                    builder = "-";
                }

                return builder;
            }
            private set { }
        }
        public List<int> genre_ids { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        private string Overview;
        public string overview
        {
            get
            {
                if (Overview == "" || Overview == null)
                {
                    return "-";
                }
                else
                {
                    return Overview;
                }
            }
            set
            {
                Overview = value;
            }
        }
        public float popularity { get; set; }

        private string Poster_path;
        public string poster_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Poster_path;
            }
            set
            {
                Poster_path = value;
            }
        }
        //public Production_Companies[] production_companies { get; set; }
        public List<Production_Companies> production_companies { private get; set; }
        public string Production_companies
        {
            get
            {
                string builder = "";

                foreach (Production_Companies pc in production_companies)
                {
                    builder += pc.name + ",\n";
                }

                if (builder.Length > 0)
                {
                    builder = builder.Substring(0, builder.Length - 2);
                }
                else
                {
                    builder = "-";
                }

                return builder;
            }
            private set { }
        }
        //public Production_Countries[] production_countries { get; set; }
        public List<Production_Countries> production_countries { get; set; }
        private string Release_date;
        public string release_date
        {
            get
            {
                if (Release_date == "" || Release_date == null)
                {
                    return "-";
                }
                else
                {
                    string str = Release_date.Replace("-", "");
                    DateTime dt = DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.InvariantCulture);
                    return dt.ToString("yyyy. MM. dd.");
                }
            }
            set
            {
                Release_date = value;
            }
        }
        private int Revenue;
        public string revenue
        {
            get
            {
                return Revenue.ToString("C0");
            }
            set
            {
                Revenue = Int32.Parse(value);
            }
        }
        private int Runtime;
        public string runtime
        {
            get
            {
                return Runtime + " mins";
            }
            set
            {
                Runtime = Int32.Parse(value);
            }
        }
        //public Spoken_Languages[] spoken_languages { get; set; }
        public List<Spoken_Languages> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public float vote_average { get; set; }
        public int vote_count { get; set; }
        public External_Ids external_ids { get; set; }
        public Videos videos { get; set; }
        public Recommendations recommendations { get; set; }
        public string getImdb_link
        {
            get
            {
                return "https://www.imdb.com/title/" + external_ids.imdb_id + "/";
            }
        }
        public string getTmdb_link
        {
            get
            {
                return "https://www.themoviedb.org/movie/" + id;
            }
        }
        public string getYoutube_link
        {
            get
            {
                if (videos.results.Count > 0)
                {
                    return "https://www.youtube.com/watch?v=" + videos.results[0].key;
                }
                else
                {
                    return "https://www.youtube.com/results?search_query=" + title + " trailer";
                }
            }
        }
        public string getFacebook_link
        {
            get
            {
                return "https://www.facebook.com/" + external_ids.facebook_id + "/";
            }
        }
        public string getInstagram_link
        {
            get
            {
                return "https://www.instagram.com/" + external_ids.instagram_id + "/";
            }
        }
        public string getTwitter_link
        {
            get
            {
                return "https://twitter.com/" + external_ids.twitter_id;
            }
        }
        public string isImdb
        {
            get
            {
                if (external_ids.imdb_id == "" || external_ids.imdb_id == null)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public string isYoutube
        {
            get
            {
                if (videos.results.Count == 0)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public string isFacebook
        {
            get
            {
                if (external_ids.facebook_id == "" || external_ids.facebook_id == null)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public string isInstagram
        {
            get
            {
                if (external_ids.instagram_id == "" || external_ids.instagram_id == null)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public string isTwitter
        {
            get
            {
                if (external_ids.twitter_id == "" || external_ids.twitter_id == null)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
            }
        }
        public Release_Dates release_dates { get; set; }
        public string certification
        {
            get
            {
                foreach(Result5 rds in release_dates.results)
                {
                    if(rds.iso_3166_1 == "US")
                    {
                        foreach(Release_Dates1 rd1 in rds.release_dates)
                        {
                            if(rd1.certification != "")
                            {
                                return rd1.certification;
                            }
                        }
                    }
                }
                return "-";
            }
        }
    }

    /*public class External_Ids
    {
        public string imdb_id { get; set; }
        public string facebook_id { get; set; }
        public object instagram_id { get; set; }
        public object twitter_id { get; set; }
    }*/

    public class Videos
    {
        //public Result[] results { get; set; }
        public List<Result> results { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string iso_639_1 { get; set; }
        public string iso_3166_1 { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string site { get; set; }
        public int size { get; set; }
        public string type { get; set; }
    }

    public class Recommendations
    {
        public int page { get; set; }
        //public Result1[] results { get; set; }
        public List<Result1> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }

    public class Result1
    {
        public int id { get; set; }
        public bool video { get; set; }
        public int vote_count { get; set; }
        public float vote_average { get; set; }
        public string title { get; set; }
        public string release_date { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        //public int[] genre_ids { get; set; }
        public List<int> genre_ids { get; set; }
        private string Backdrop_path;
        public string backdrop_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Backdrop_path;
            }
            set
            {
                Backdrop_path = value;
            }
        }
        public bool adult { get; set; }
        public string overview { get; set; }
        private string Poster_path;
        public string poster_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Poster_path;
            }
            set
            {
                Poster_path = value;
            }
        }
        public float popularity { get; set; }
        //--------
        public string first_air_date { get; set; }
        public string name { get; set; }
        //public string[] origin_country { get; set; }
        public List<string> origin_country { get; set; }
        public string original_name { get; set; }
        //public Network[] networks { get; set; }
        public List<Network> networks { get; set; }
    }

    public class Genre
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Production_Companies
    {
        public int id { get; set; }
        private string Logo_path;
        public string logo_path
        {
            get
            {
                return "http://image.tmdb.org/t/p/original/" + Logo_path;
            }
            set
            {
                Logo_path = value;
            }
        }
        public string name { get; set; }
        public string origin_country { get; set; }
    }

    public class Production_Countries
    {
        public string iso_3166_1 { get; set; }
        public string name { get; set; }
    }

    public class Spoken_Languages
    {
        public string iso_639_1 { get; set; }
        public string name { get; set; }
    }

    public class Release_Dates
    {
        //public Result5[] results { get; set; }
        public List<Result5> results { get; set; }
    }

    public class Result5
    {
        public string iso_3166_1 { get; set; }
        //public Release_Dates1[] release_dates { get; set; }
        public List<Release_Dates1> release_dates { get; set; }
    }

    public class Release_Dates1
    {
        public string certification { get; set; }
        public string iso_639_1 { get; set; }
        public DateTime release_date { get; set; }
        public int type { get; set; }
        public string note { get; set; }
    }

}