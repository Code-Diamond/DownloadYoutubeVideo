using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace DownloadYoutubeVideo
{
    class Program
    {
        static void Main(string[] args)
        {
            download();
            
            Console.ReadKey();
        }

        public static async void download()
        {
            var client = new YoutubeClient();

            // Get metadata for all streams in this video
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync("XnGPN7UBOFQ");

            // Select one of the streams, e.g. highest quality muxed stream
            var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();

                // ...or highest bitrate audio stream
                // var streamInfo = streamInfoSet.Audio.WithHighestBitrate();

                // ...or highest quality & highest framerate MP4 video stream
                // var streamInfo = streamInfoSet.Video
                //    .Where(s => s.Container == Container.Mp4)
                //    .OrderByDescending(s => s.VideoQuality)
                //    .ThenByDescending(s => s.Framerate)
                //    .First();

            // Get file extension based on stream's container
            var ext = streamInfo.Container.GetFileExtension();

            // Download stream to file -> in debug folder
            await client.DownloadMediaStreamAsync(streamInfo, $"downloaded_video.{ext}");
        }
    }
}
