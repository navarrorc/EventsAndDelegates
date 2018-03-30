using System;
using System.Threading;

namespace EventsAndDelegates
{
    class Program
    {
        static void Main()
        {
            var video = new Video { Title = "Video 1" };

            var video2 = new Video { Title = "Video 2" };



            var videoEncoder = new VideoEncoder(); // publisher
            var mailService = new MailService(); // subscriber
            var messageService = new MessageService(); // subscriber

            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded;

            videoEncoder.Encode(video);
            videoEncoder.Encode(video2);

            Console.WriteLine("Press [Enter] to close application.");
            Console.ReadLine();
        }
    }

    class VideoEventArgs : EventArgs
    {
        public Video Clip { get; set; }
    }

    class MailService
    {
        public void OnVideoEncoded(object source, VideoEventArgs args)
        {
            Console.WriteLine(args.Clip.Title + " - MailService: Sending an email...");
        }
    }

    class MessageService
    {
        public void OnVideoEncoded(object source, VideoEventArgs args)
        {
            Console.WriteLine(args.Clip.Title + " - MessageService: Sending a text message...");
        }
    }

    class VideoEncoder
    {
        // 1. Define a delegate
        // 2. Define an event based on the delegate
        // 3. Raise the event

        public delegate void VideoEncodedEventHandler(object source, VideoEventArgs args);

        public event VideoEncodedEventHandler VideoEncoded;

        public void Encode(Video video)
        {
            Console.WriteLine("Encoding Video...");
            Thread.Sleep(3000);

            OnVideoEncoded(video);
        }

        protected virtual void OnVideoEncoded(Video video)
        {
            //if (VideoEncoded != null)
            //{
            //    VideoEncoded(this, EventArgs.Empty);
            //}
            var vEvents = new VideoEventArgs { Clip = video };

            VideoEncoded?.Invoke(this, vEvents); // see: http://bit.ly/2ujCs1F
        }
    }

    class Video
    {
        public string Title { get; set; }
    }
}
