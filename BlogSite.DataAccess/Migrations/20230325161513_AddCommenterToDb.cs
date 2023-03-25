using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogSite.Migrations
{
    /// <inheritdoc />
    public partial class AddCommenterToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_BlogPosts_BlogPostId",
                table: "PostComments");

            migrationBuilder.RenameColumn(
                name: "CommenterEmail",
                table: "PostComments",
                newName: "Commenter");

            migrationBuilder.AlterColumn<int>(
                name: "BlogPostId",
                table: "PostComments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "PostComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_BlogPosts_BlogPostId",
                table: "PostComments",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_BlogPosts_BlogPostId",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "PostComments");

            migrationBuilder.RenameColumn(
                name: "Commenter",
                table: "PostComments",
                newName: "CommenterEmail");

            migrationBuilder.AlterColumn<int>(
                name: "BlogPostId",
                table: "PostComments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_BlogPosts_BlogPostId",
                table: "PostComments",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
