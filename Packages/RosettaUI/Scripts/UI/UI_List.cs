﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RosettaUI
{
    public static partial class UI
    {
        public static Element List<T>(Expression<Func<IList<T>>> targetExpression, Func<IBinder<T>, int, Element> createItemElement = null)
        {
            var labelString = ExpressionUtility.CreateLabelString(targetExpression);
            return List(labelString, targetExpression, createItemElement);
        }
        
        public static Element List<T>(LabelElement label, Expression<Func<IList<T>>> targetExpression, Func<IBinder<T>, int, Element> createItemElement = null)
        {
            var binder = ExpressionUtility.CreateBinder(targetExpression);
            var createItemElementIBinder = createItemElement == null
                ? (Func<IBinder, int, Element>)null
                : (ib, idx) => createItemElement(ib as IBinder<T>, idx);

            return List(label, binder, createItemElementIBinder);
        }

        public static Element List(LabelElement label, IBinder listBinder, Func<IBinder, int, Element> createItemElement = null)
        {
            var isReadOnly = ListBinder.IsReadOnly(listBinder);

            var countFieldWidth = 50f;
            var field = Field("",
                () => ListBinder.GetCount(listBinder),
                isReadOnly ? (Action<int>) null : (count) => ListBinder.SetCount(listBinder, count)
            ).SetWidth(countFieldWidth);
     
            var buttonWidth = 30f;
            var buttons = isReadOnly
                ? null
                : Row(
                    Button("＋", () => ListBinder.AddItemAtLast(listBinder)).SetWidth(buttonWidth),
                    Button("－", () => ListBinder.RemoveItemAtLast(listBinder)).SetWidth(buttonWidth)
                ).SetJustify(Style.Justify.End);

            return Fold(
                barLeft: label,
                barRight: Row(field),
                elements: new[]
                {
                    Box(
                        List(listBinder, createItemElement),
                        buttons
                    )
                }
            );
        }
        
        public static Element List(IBinder listBinder, Func<IBinder, int, Element> createItemElement = null)
        {
            return DynamicElementOnStatusChanged(
                readStatus: () => ListBinder.GetCount(listBinder),
                build: _ =>
                {
                    createItemElement ??= ((binder, idx) => Field("Item " + idx, binder));

                    var itemBinderToElement = createItemElement;

                    if (!ListBinder.IsReadOnly(listBinder))
                    {
                        itemBinderToElement = (binder,idx) => {
                            var element = Popup(
                                createItemElement(binder, idx),
                                () => new[]
                                {
                                    new MenuItem("Add Element", () => ListBinder.DuplicateItem(listBinder, idx)),
                                    new MenuItem("Remove Element", () => ListBinder.RemoveItem(listBinder, idx)),
                                }
                            );
                            
                            return element;
                        };
                    }

                    return Column(
                        ListBinder.CreateItemBinders(listBinder).Select(itemBinderToElement)
                    );
                });
        }
    }
}