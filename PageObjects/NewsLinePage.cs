using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;
using ATframework3demo.TestEntities;

namespace ATframework3demo.PageObjects
{

    public class NewsLinePage
    {
        public PortalTopMenu TopMenu => new PortalTopMenu();
        public static WebItem PostLink(string postTitle) =>
            new WebItem($"//a[text()='{postTitle}']", $"Ссылка на пост {postTitle}");
        public PostPage OpenPost(Post post)
        {
            PostLink(post.Title).Click();
            return new PostPage();
        }
    }
}


