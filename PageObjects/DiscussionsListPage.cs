using ATframework3demo.TestEntities;
using atFrameWork2.PageObjects;
using atFrameWork2.SeleniumFramework;

namespace ATframework3demo.PageObjects
{
    public class DiscussionsListPage
    {
        public PostPage OpenPost(Post post)
        {
            // открыть пост из списка обсуждений
            NewsLinePage.PostLink(post.Title).Click();
            return new PostPage();
        }
    }
}
