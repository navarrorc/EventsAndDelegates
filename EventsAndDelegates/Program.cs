using System;
using System.Threading;

namespace EventsAndDelegates
{
    class Program
    {
        static void Main()
        {
            var video = new Video() { Title = "Video 1" };
            var videoEncoder = new VideoEncoder(); // publisher
            var mailService = new MailService(); // subscriber
            var messageService = new MessageService(); // subscriber

            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded;

            videoEncoder.Encode(video);

            Console.WriteLine("Press [Enter] to close application.");
            Console.ReadLine();
        }
    }

    class MailService
    {
        public void OnVideoEncoded(object source, EventArgs e)
        {
            Console.WriteLine("MailService: Sending an email...");
        }
    }

    class MessageService
    {
        public void OnVideoEncoded(object source, EventArgs e)
        {
            Console.WriteLine("MessageService: Sending a text message...");
        }
    }

    class VideoEncoder
    {
        // 1. Define a delegate
        // 2. Define an event based on the delegate
        // 3. Raise the event

        public delegate void VideoEncodedEventHandler(object source, EventArgs args);

        public event VideoEncodedEventHandler VideoEncoded;

        public void Encode(Video video)
        {
            Console.WriteLine("Encoding Video...");
            Thread.Sleep(3000);

            OnVideoEncoded();
        }

        protected virtual void OnVideoEncoded()
        {
            //if (VideoEncoded != null)
            //{
            //    VideoEncoded(this, EventArgs.Empty);
            //}

            VideoEncoded?.Invoke(this, EventArgs.Empty); // see: http://bit.ly/2ujCs1F
        }
    }

    class Video
    {
        public string Title { get; set; }
    }
}
