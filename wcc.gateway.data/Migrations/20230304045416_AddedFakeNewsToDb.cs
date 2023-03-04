using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedFakeNewsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "news",
                columns: new[] { "Name", "Description", "ImageUrl" },
                values: new object[,]
                {
                    { "What is Lorem Ipsum?", "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s,", "https://i.picsum.photos/id/664/140/345.jpg?hmac=652GUsIUg395zIDLbpwU2hfstk5GQsLVMNFjyu7OIAc" },
                    { "Why do we use it?", "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.", "https://i.picsum.photos/id/965/140/345.jpg?hmac=7-_CkBhBFDClyLYILPdeGyeBtmiRBVgBRexfaduhvo4" },
                    { "Where does it come from?", "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. ", "https://i.picsum.photos/id/387/140/345.jpg?hmac=hZNcO1Q71wsCZBt0gsUxljbR4sKs8VuVHFt0YNZxOq0" },
                    { "Lorem ipsum dolor sit amet.", "Ea quos minima eum laudantium eaque sit corrupti tempora. At suscipit doloribus ut iure voluptatibus qui odit tenetur sit voluptatem odio hic sint ipsam cum consequatur architecto. ", "https://i.picsum.photos/id/87/387/140.jpg?hmac=GyXj2rw58Fc5tw6vFhPoOZbBOhgYNH5x1cHU8A2HLqU" },
                    { "Eos vero voluptas", "At adipisci quos et voluptatem consequatur non dolor impedit. Ut similique iste et eaque quia ut ipsa neque aut quia quis ea maxime magnam.", "https://i.picsum.photos/id/79/387/140.jpg?hmac=j4tWMWNUA8ZMAmY83Mj16VODn_aVR5IDGc81yMVGj_E" },
                    { "Rem officiis quod ut repellat", " At totam odio sed temporibus voluptatem eos natus temporibus sit enim porro. Vel aperiam ipsum quo vitae nihil et fugiat iure! Aut dolorem quisquam est alias cupiditate ut culpa dolorem nam deleniti architecto ea itaque molestiae.", "https://i.picsum.photos/id/591/387/140.jpg?hmac=v3SIsk5v-Nq_jeSyterdQtVRAIONY5VFhCO39GpCP2s" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
