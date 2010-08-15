using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shell;

namespace WpfApplication1 {
    public class TaskbarItemOverlay  {
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.RegisterAttached("Content", typeof(object), typeof(TaskbarItemOverlay), new PropertyMetadata(OnPropertyChanged));

        public static readonly DependencyProperty TemplateProperty =
            DependencyProperty.RegisterAttached("Template", typeof(DataTemplate), typeof(TaskbarItemOverlay), new PropertyMetadata(OnPropertyChanged));


        public static object GetContent(DependencyObject dependencyObject) {
            return dependencyObject.GetValue(ContentProperty);
        }

        public static void SetContent(DependencyObject dependencyObject, object content) {
            dependencyObject.SetValue(ContentProperty, content);
        }

        public static DataTemplate GetTemplate(DependencyObject dependencyObject) {
            return (DataTemplate)dependencyObject.GetValue(TemplateProperty);
        }

        public static void SetTemplate(DependencyObject dependencyObject, DataTemplate template) {
            dependencyObject.SetValue(TemplateProperty, template);
        }

        private static void OnPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e) {
            var taskbarItemInfo = (TaskbarItemInfo) dependencyObject;
            var content = GetContent(taskbarItemInfo);
            var template = GetTemplate(taskbarItemInfo);

            if (template == null || content == null) {
                taskbarItemInfo.Overlay = null;
                return;
            }

            const int ICON_WIDTH = 16;
            const int ICON_HEIGHT = 16;

            var bmp =
                new RenderTargetBitmap(ICON_WIDTH, ICON_HEIGHT, 96, 96, PixelFormats.Default);
            var root = new ContentControl {
                ContentTemplate = template, 
                Content = content
            };
            root.Arrange(new Rect(0, 0, ICON_WIDTH, ICON_HEIGHT));
            bmp.Render(root);

            taskbarItemInfo.Overlay = bmp;
        }
    }
}