using Minesweeper.BusinessLogic;

namespace Minesweeper.Test.Models
{
    // TODO: Create more properties that return values from the logic model
    class FieldModel : IModel
    {
        public FieldModel(Model logicModel)
        {
            Original = logicModel;
        }

        public Model Original { get; }

        public FieldStatus Status
        {
            get => Original.Status;
            set => Original.Status = value;
        }

        public FieldValues Value
        {
            get => Original.Value;
            set => Original.Value = value;
        }

        // TODO: Change property name to "Content". It should contain an image
        public string Text
        {
            get
            {
                if (Original.Status == FieldStatus.Covered)
                {
                    return "";
                }
                else if (Original.Status == FieldStatus.Flag)
                {
                    return "F";
                }
                else if (Original.Status == FieldStatus.QuestionMark)
                {
                    return "?";
                }
                else if (Original.Status == FieldStatus.Uncovered)
                {
                    return Original.Value switch
                    {
                        FieldValues.Mine => "M",
                        FieldValues.Empty => "",
                        _ => ((int)Original.Value).ToString(),
                    };
                }

                return "ERROR";
            }
        }

        public bool IsEnabled => Original.Status != FieldStatus.Uncovered;

        public bool Compare(Model model) => Original == model;
    }
}
