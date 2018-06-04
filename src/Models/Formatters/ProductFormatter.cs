using System;
using System.Collections.Generic;
using System.Text;
using MessagePack;
using MessagePack.Formatters;

namespace Models.Formatters
{
    public class ProductFormatter : IMessagePackFormatter<Product>
    {
        public ProductFormatter()
        {
        }

        public Product Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            int startOffset = offset;
            int size = 0;
            Product p = new Product();
            _ = MessagePackBinary.ReadMapHeader(bytes, offset, out size);
            offset += size;

            string fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;

            p.Id = MessagePackBinary.ReadInt64(bytes, offset, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;

            p.Name = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            p.Description = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            p.Seller = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            formatterResolver.GetFormatter<Category>().Deserialize(bytes, offset, formatterResolver, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            p.Rating = formatterResolver.GetFormatter<Rate>().Deserialize(bytes, offset,formatterResolver, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            if (bytes[offset] != 0xc0)
            {
                string price = MessagePackBinary.ReadString(bytes, offset, out size);
                decimal priceResult;
                if (Decimal.TryParse(price, out priceResult))
                {
                    p.Price = priceResult;
                };
            }   
            else
                MessagePackBinary.ReadNil(bytes, offset, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            p.Currency = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            p.Created = MessagePackBinary.ReadDateTime(bytes, offset, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            if (bytes[offset] != 0xc0)
                p.Updated = MessagePackBinary.ReadDateTime(bytes, offset, out size);
            else
                MessagePackBinary.ReadNil(bytes, offset, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            p.Labels = formatterResolver.GetFormatter<ICollection<string>>().Deserialize(bytes, offset, formatterResolver, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            Uri uriStr = formatterResolver.GetFormatter<Uri>().Deserialize(bytes, offset, formatterResolver, out size);
            p.Location = uriStr;
            offset += size;
            
            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;
            p.Image =  MessagePackBinary.ReadBytes(bytes, offset, out size);
            offset += size;

            fieldName = MessagePackBinary.ReadString(bytes, offset, out size);
            offset += size;

            p.Details = formatterResolver.GetFormatter<Dictionary<string, string>>().Deserialize(bytes, offset, formatterResolver, out size);
            offset += size;

            readSize = offset - startOffset;
            return p;
        }

        public int Serialize(ref byte[] bytes, int offset, Product value, IFormatterResolver formatterResolver)
        {
            int startOffset = offset;
            offset += MessagePackBinary.WriteMapHeader(ref bytes, offset, 14);

            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Id));
            offset += MessagePackBinary.WriteInt64(ref bytes, offset, value.Id);

            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Name));
            offset += MessagePackBinary.WriteString(ref bytes, offset, value.Name);

            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Description));
            offset += MessagePackBinary.WriteString(ref bytes, offset, value.Description);

            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Seller));
            offset += MessagePackBinary.WriteString(ref bytes, offset, value.Seller);
             
            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Category));
            offset += formatterResolver.GetFormatter<Category>().Serialize(ref bytes, offset, value.Category, formatterResolver);

            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Rating));
            offset += formatterResolver.GetFormatter<Rate>().Serialize(ref bytes, offset, value.Rating, formatterResolver);
             
            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Price));
            if (!value.Price.HasValue)
                offset += MessagePackBinary.WriteNil(ref bytes, offset);
            else
            { 
                offset += MessagePackBinary.WriteString(ref bytes, offset, value.Price.Value.ToString());
            }
                


            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Currency));
            offset += MessagePackBinary.WriteString(ref bytes, offset, value.Currency);

            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Created));
            offset += MessagePackBinary.WriteDateTime(ref bytes, offset, value.Created);

            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Updated));
            if (!value.Updated.HasValue)
                offset += MessagePackBinary.WriteNil(ref bytes, offset);
            else
                offset += MessagePackBinary.WriteDateTime(ref bytes, offset, value.Updated.Value);

            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Labels));
            offset += formatterResolver.GetFormatter<ICollection<string>>().Serialize(ref bytes, offset, value.Labels, formatterResolver);

            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Location));
            offset += formatterResolver.GetFormatter<Uri>().Serialize(ref bytes, offset, value.Location, formatterResolver);


            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Image));
            offset += MessagePackBinary.WriteBytes(ref bytes, offset, value.Image);

            offset += MessagePackBinary.WriteString(ref bytes, offset, nameof(value.Details));
            offset += formatterResolver.GetFormatter<Dictionary<string, string>>().Serialize(ref bytes, offset, value.Details, formatterResolver);
            
            return offset - startOffset;
        }
    }
}
