using System.Windows.Forms;
using WeSay.Language;
using WeSay.LexicalModel;
using WeSay.Project;
using WeSay.UI;
using System.ComponentModel;

namespace WeSay.LexicalTools
{
	/// <summary>
	/// <see cref="Layouter"/>
	/// </summary>
	public class LexEntryLayouter : Layouter
	{
		public LexEntryLayouter(DetailList builder, ViewTemplate viewTemplate)
			: base(builder, viewTemplate)
		{
		}

		public override int AddWidgets(IBindingList list, int index)
		{
			return AddWidgets(list, index, -1);
		}

		internal override int AddWidgets(IBindingList list, int index, int insertAtRow)
		{
			return AddWidgets((LexEntry)list[index], insertAtRow);
		}

		public int AddWidgets(LexEntry entry)
		{
			return AddWidgets(entry, -1);
		}

		internal int AddWidgets(LexEntry entry, int insertAtRow)
		{
			int rowCount = 0;
			Field field = ViewTemplate.GetField(Field.FieldNames.EntryLexicalForm.ToString());
			if (field != null && field.Visibility == Field.VisibilitySetting.Visible)
			{
				Control box = MakeBoundEntry(entry.LexicalForm, field);
				DetailList.AddWidgetRow(StringCatalog.Get("Word"), true, box, insertAtRow);
				++rowCount;
			}
			LexSenseLayouter layouter = new LexSenseLayouter(DetailList, ViewTemplate);
			rowCount = AddChildrenWidgets(layouter, entry.Senses, insertAtRow, rowCount);
			//add a ghost
			rowCount += layouter.AddGhost(entry.Senses);

			return rowCount;
		}


	}
}