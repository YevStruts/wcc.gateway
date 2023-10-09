﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountryCodeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Code",
                table: "Country",
                type: "nvarchar(6)",
                nullable: false,
                defaultValue: "xx");

            migrationBuilder.Sql(@"
UPDATE Country SET [Name] = 'Afghanistan', [Code] = 'af' WHERE Id = 1;
UPDATE Country SET [Name] = 'Albania', [Code] = 'al' WHERE Id = 2;
UPDATE Country SET [Name] = 'Algeria', [Code] = 'dz' WHERE Id = 3;
UPDATE Country SET [Name] = 'American Samoa', [Code] = 'as' WHERE Id = 4;
UPDATE Country SET [Name] = 'Andorra', [Code] = 'ad' WHERE Id = 5;
UPDATE Country SET [Name] = 'Angola', [Code] = 'ao' WHERE Id = 6;
UPDATE Country SET [Name] = 'Anguilla', [Code] = 'ai' WHERE Id = 7;
UPDATE Country SET [Name] = 'Antarctica', [Code] = 'aq' WHERE Id = 8;
UPDATE Country SET [Name] = 'Antigua and Barbuda', [Code] = 'ag' WHERE Id = 9;
UPDATE Country SET [Name] = 'Argentina', [Code] = 'ar' WHERE Id = 10;
UPDATE Country SET [Name] = 'Armenia', [Code] = 'am' WHERE Id = 11;
UPDATE Country SET [Name] = 'Aruba', [Code] = 'aw' WHERE Id = 12;
UPDATE Country SET [Name] = 'Australia', [Code] = 'au' WHERE Id = 13;
UPDATE Country SET [Name] = 'Austria', [Code] = 'at' WHERE Id = 14;
UPDATE Country SET [Name] = 'Azerbaijan', [Code] = 'az' WHERE Id = 15;
UPDATE Country SET [Name] = 'Bahamas', [Code] = 'bs' WHERE Id = 16;
UPDATE Country SET [Name] = 'Bahrain', [Code] = 'bh' WHERE Id = 17;
UPDATE Country SET [Name] = 'Bangladesh', [Code] = 'bd' WHERE Id = 18;
UPDATE Country SET [Name] = 'Barbados', [Code] = 'bb' WHERE Id = 19;
UPDATE Country SET [Name] = 'Belarus', [Code] = 'by' WHERE Id = 20;
UPDATE Country SET [Name] = 'Belgium', [Code] = 'be' WHERE Id = 21;
UPDATE Country SET [Name] = 'Belize', [Code] = 'bz' WHERE Id = 22;
UPDATE Country SET [Name] = 'Benin', [Code] = 'bj' WHERE Id = 23;
UPDATE Country SET [Name] = 'Bermuda', [Code] = 'bm' WHERE Id = 24;
UPDATE Country SET [Name] = 'Bhutan', [Code] = 'bt' WHERE Id = 25;
UPDATE Country SET [Name] = 'Bolivia', [Code] = 'bo' WHERE Id = 26;
UPDATE Country SET [Name] = 'Bonaire, Sint Eustatius and Saba', [Code] = 'bq' WHERE Id = 27;
UPDATE Country SET [Name] = 'Bosnia and Herzegovina', [Code] = 'ba' WHERE Id = 28;
UPDATE Country SET [Name] = 'Botswana', [Code] = 'bw' WHERE Id = 29;
UPDATE Country SET [Name] = 'Bouvet Island', [Code] = 'bv' WHERE Id = 30;
UPDATE Country SET [Name] = 'Brazil', [Code] = 'br' WHERE Id = 31;
UPDATE Country SET [Name] = 'British Indian Ocean Territory', [Code] = 'io' WHERE Id = 32;
UPDATE Country SET [Name] = 'Brunei Darussalam', [Code] = 'bn' WHERE Id = 33;
UPDATE Country SET [Name] = 'Bulgaria', [Code] = 'bg' WHERE Id = 34;
UPDATE Country SET [Name] = 'Burkina Faso', [Code] = 'bf' WHERE Id = 35;
UPDATE Country SET [Name] = 'Burundi', [Code] = 'bi' WHERE Id = 36;
UPDATE Country SET [Name] = 'Cabo Verde', [Code] = 'cv' WHERE Id = 37;
UPDATE Country SET [Name] = 'Cambodia', [Code] = 'kh' WHERE Id = 38;
UPDATE Country SET [Name] = 'Cameroon', [Code] = 'cm' WHERE Id = 39;
UPDATE Country SET [Name] = 'Canada', [Code] = 'ca' WHERE Id = 40;
UPDATE Country SET [Name] = 'Cayman Islands', [Code] = 'ky' WHERE Id = 41;
UPDATE Country SET [Name] = 'Central African Republic', [Code] = 'cf' WHERE Id = 42;
UPDATE Country SET [Name] = 'Chad', [Code] = 'td' WHERE Id = 43;
UPDATE Country SET [Name] = 'Chile', [Code] = 'cl' WHERE Id = 44;
UPDATE Country SET [Name] = 'China', [Code] = 'cn' WHERE Id = 45;
UPDATE Country SET [Name] = 'Christmas Island', [Code] = 'cx' WHERE Id = 46;
UPDATE Country SET [Name] = 'Cocos Islands', [Code] = 'cc' WHERE Id = 47;
UPDATE Country SET [Name] = 'Colombia', [Code] = 'co' WHERE Id = 48;
UPDATE Country SET [Name] = 'Comoros', [Code] = 'km' WHERE Id = 49;
UPDATE Country SET [Name] = 'Congo', [Code] = 'cd' WHERE Id = 50;
UPDATE Country SET [Name] = 'Congo', [Code] = 'cg' WHERE Id = 51;
UPDATE Country SET [Name] = 'Cook Islands', [Code] = 'ck' WHERE Id = 52;
UPDATE Country SET [Name] = 'Costa Rica', [Code] = 'cr' WHERE Id = 53;
UPDATE Country SET [Name] = 'Croatia', [Code] = 'hr' WHERE Id = 54;
UPDATE Country SET [Name] = 'Cuba', [Code] = 'cu' WHERE Id = 55;
UPDATE Country SET [Name] = 'Curaçao', [Code] = 'cw' WHERE Id = 56;
UPDATE Country SET [Name] = 'Cyprus', [Code] = 'cy' WHERE Id = 57;
UPDATE Country SET [Name] = 'Czechia', [Code] = 'cz' WHERE Id = 58;
UPDATE Country SET [Name] = 'Côte d''Ivoire', [Code] = 'ci' WHERE Id = 59;
UPDATE Country SET [Name] = 'Denmark', [Code] = 'dk' WHERE Id = 60;
UPDATE Country SET [Name] = 'Djibouti', [Code] = 'dj' WHERE Id = 61;
UPDATE Country SET [Name] = 'Dominica', [Code] = 'dm' WHERE Id = 62;
UPDATE Country SET [Name] = 'Dominican Republic', [Code] = 'do' WHERE Id = 63;
UPDATE Country SET [Name] = 'Ecuador', [Code] = 'ec' WHERE Id = 64;
UPDATE Country SET [Name] = 'Egypt', [Code] = 'eg' WHERE Id = 65;
UPDATE Country SET [Name] = 'El Salvador', [Code] = 'sv' WHERE Id = 66;
UPDATE Country SET [Name] = 'Equatorial Guinea', [Code] = 'gq' WHERE Id = 67;
UPDATE Country SET [Name] = 'Eritrea', [Code] = 'er' WHERE Id = 68;
UPDATE Country SET [Name] = 'Estonia', [Code] = 'ee' WHERE Id = 69;
UPDATE Country SET [Name] = 'Eswatini', [Code] = 'sz' WHERE Id = 70;
UPDATE Country SET [Name] = 'Ethiopia', [Code] = 'et' WHERE Id = 71;
UPDATE Country SET [Name] = 'Falkland Islands [Malvinas]', [Code] = 'fk' WHERE Id = 72;
UPDATE Country SET [Name] = 'Faroe Islands', [Code] = 'fo' WHERE Id = 73;
UPDATE Country SET [Name] = 'Fiji', [Code] = 'fj' WHERE Id = 74;
UPDATE Country SET [Name] = 'Finland', [Code] = 'fi' WHERE Id = 75;
UPDATE Country SET [Name] = 'France', [Code] = 'fr' WHERE Id = 76;
UPDATE Country SET [Name] = 'French Guiana', [Code] = 'gf' WHERE Id = 77;
UPDATE Country SET [Name] = 'French Polynesia', [Code] = 'pf' WHERE Id = 78;
UPDATE Country SET [Name] = 'French Southern Territories', [Code] = 'tf' WHERE Id = 79;
UPDATE Country SET [Name] = 'Gabon', [Code] = 'ga' WHERE Id = 80;
UPDATE Country SET [Name] = 'Gambia', [Code] = 'gm' WHERE Id = 81;
UPDATE Country SET [Name] = 'Georgia', [Code] = 'ge' WHERE Id = 82;
UPDATE Country SET [Name] = 'Germany', [Code] = 'de' WHERE Id = 83;
UPDATE Country SET [Name] = 'Ghana', [Code] = 'gh' WHERE Id = 84;
UPDATE Country SET [Name] = 'Gibraltar', [Code] = 'gi' WHERE Id = 85;
UPDATE Country SET [Name] = 'Greece', [Code] = 'gr' WHERE Id = 86;
UPDATE Country SET [Name] = 'Greenland', [Code] = 'gl' WHERE Id = 87;
UPDATE Country SET [Name] = 'Grenada', [Code] = 'gd' WHERE Id = 88;
UPDATE Country SET [Name] = 'Guadeloupe', [Code] = 'gp' WHERE Id = 89;
UPDATE Country SET [Name] = 'Guam', [Code] = 'gu' WHERE Id = 90;
UPDATE Country SET [Name] = 'Guatemala', [Code] = 'gt' WHERE Id = 91;
UPDATE Country SET [Name] = 'Guernsey', [Code] = 'gg' WHERE Id = 92;
UPDATE Country SET [Name] = 'Guinea', [Code] = 'gn' WHERE Id = 93;
UPDATE Country SET [Name] = 'Guinea-Bissau', [Code] = 'gw' WHERE Id = 94;
UPDATE Country SET [Name] = 'Guyana', [Code] = 'gy' WHERE Id = 95;
UPDATE Country SET [Name] = 'Haiti', [Code] = 'ht' WHERE Id = 96;
UPDATE Country SET [Name] = 'Heard Island and McDonald Islands', [Code] = 'hm' WHERE Id = 97;
UPDATE Country SET [Name] = 'Holy See', [Code] = 'va' WHERE Id = 98;
UPDATE Country SET [Name] = 'Honduras', [Code] = 'hn' WHERE Id = 99;
UPDATE Country SET [Name] = 'Hong Kong', [Code] = 'hk' WHERE Id = 100;
UPDATE Country SET [Name] = 'Hungary', [Code] = 'hu' WHERE Id = 101;
UPDATE Country SET [Name] = 'Iceland', [Code] = 'is' WHERE Id = 102;
UPDATE Country SET [Name] = 'India', [Code] = 'in' WHERE Id = 103;
UPDATE Country SET [Name] = 'Indonesia', [Code] = 'id' WHERE Id = 104;
UPDATE Country SET [Name] = 'Iran (Islamic Republic of)', [Code] = 'ir' WHERE Id = 105;
UPDATE Country SET [Name] = 'Iraq', [Code] = 'iq' WHERE Id = 106;
UPDATE Country SET [Name] = 'Ireland', [Code] = 'ie' WHERE Id = 107;
UPDATE Country SET [Name] = 'Isle of Man', [Code] = 'im' WHERE Id = 108;
UPDATE Country SET [Name] = 'Israel', [Code] = 'il' WHERE Id = 109;
UPDATE Country SET [Name] = 'Italy', [Code] = 'it' WHERE Id = 110;
UPDATE Country SET [Name] = 'Jamaica', [Code] = 'jm' WHERE Id = 111;
UPDATE Country SET [Name] = 'Japan', [Code] = 'jp' WHERE Id = 112;
UPDATE Country SET [Name] = 'Jersey', [Code] = 'je' WHERE Id = 113;
UPDATE Country SET [Name] = 'Jordan', [Code] = 'jo' WHERE Id = 114;
UPDATE Country SET [Name] = 'Kazakhstan', [Code] = 'kz' WHERE Id = 115;
UPDATE Country SET [Name] = 'Kenya', [Code] = 'ke' WHERE Id = 116;
UPDATE Country SET [Name] = 'Kiribati', [Code] = 'ki' WHERE Id = 117;
UPDATE Country SET [Name] = 'North Korea', [Code] = 'kp' WHERE Id = 118;
UPDATE Country SET [Name] = 'South Korea', [Code] = 'kr' WHERE Id = 119;
UPDATE Country SET [Name] = 'Kuwait', [Code] = 'kw' WHERE Id = 120;
UPDATE Country SET [Name] = 'Kyrgyzstan', [Code] = 'kg' WHERE Id = 121;
UPDATE Country SET [Name] = 'Lao People''s Democratic Republic', [Code] = 'la' WHERE Id = 122;
UPDATE Country SET [Name] = 'Latvia', [Code] = 'lv' WHERE Id = 123;
UPDATE Country SET [Name] = 'Lebanon', [Code] = 'lb' WHERE Id = 124;
UPDATE Country SET [Name] = 'Lesotho', [Code] = 'ls' WHERE Id = 125;
UPDATE Country SET [Name] = 'Liberia', [Code] = 'lr' WHERE Id = 126;
UPDATE Country SET [Name] = 'Libya', [Code] = 'ly' WHERE Id = 127;
UPDATE Country SET [Name] = 'Liechtenstein', [Code] = 'li' WHERE Id = 128;
UPDATE Country SET [Name] = 'Lithuania', [Code] = 'lt' WHERE Id = 129;
UPDATE Country SET [Name] = 'Luxembourg', [Code] = 'lu' WHERE Id = 130;
UPDATE Country SET [Name] = 'Macao', [Code] = 'mo' WHERE Id = 131;
UPDATE Country SET [Name] = 'Madagascar', [Code] = 'mg' WHERE Id = 132;
UPDATE Country SET [Name] = 'Malawi', [Code] = 'mw' WHERE Id = 133;
UPDATE Country SET [Name] = 'Malaysia', [Code] = 'my' WHERE Id = 134;
UPDATE Country SET [Name] = 'Maldives', [Code] = 'mv' WHERE Id = 135;
UPDATE Country SET [Name] = 'Mali', [Code] = 'ml' WHERE Id = 136;
UPDATE Country SET [Name] = 'Malta', [Code] = 'mt' WHERE Id = 137;
UPDATE Country SET [Name] = 'Marshall Islands', [Code] = 'mh' WHERE Id = 138;
UPDATE Country SET [Name] = 'Martinique', [Code] = 'mq' WHERE Id = 139;
UPDATE Country SET [Name] = 'Mauritania', [Code] = 'mr' WHERE Id = 140;
UPDATE Country SET [Name] = 'Mauritius', [Code] = 'mu' WHERE Id = 141;
UPDATE Country SET [Name] = 'Mayotte', [Code] = 'yt' WHERE Id = 142;
UPDATE Country SET [Name] = 'Mexico', [Code] = 'mx' WHERE Id = 143;
UPDATE Country SET [Name] = 'Micronesia', [Code] = 'fm' WHERE Id = 144;
UPDATE Country SET [Name] = 'Moldova', [Code] = 'md' WHERE Id = 145;
UPDATE Country SET [Name] = 'Monaco', [Code] = 'mc' WHERE Id = 146;
UPDATE Country SET [Name] = 'Mongolia', [Code] = 'mn' WHERE Id = 147;
UPDATE Country SET [Name] = 'Montenegro', [Code] = 'me' WHERE Id = 148;
UPDATE Country SET [Name] = 'Montserrat', [Code] = 'ms' WHERE Id = 149;
UPDATE Country SET [Name] = 'Morocco', [Code] = 'ma' WHERE Id = 150;
UPDATE Country SET [Name] = 'Mozambique', [Code] = 'mz' WHERE Id = 151;
UPDATE Country SET [Name] = 'Myanmar', [Code] = 'mm' WHERE Id = 152;
UPDATE Country SET [Name] = 'Namibia', [Code] = 'na' WHERE Id = 153;
UPDATE Country SET [Name] = 'Nauru', [Code] = 'nr' WHERE Id = 154;
UPDATE Country SET [Name] = 'Nepal', [Code] = 'np' WHERE Id = 155;
UPDATE Country SET [Name] = 'Netherlands', [Code] = 'nl' WHERE Id = 156;
UPDATE Country SET [Name] = 'New Caledonia', [Code] = 'nc' WHERE Id = 157;
UPDATE Country SET [Name] = 'New Zealand', [Code] = 'nz' WHERE Id = 158;
UPDATE Country SET [Name] = 'Nicaragua', [Code] = 'ni' WHERE Id = 159;
UPDATE Country SET [Name] = 'Niger', [Code] = 'ne' WHERE Id = 160;
UPDATE Country SET [Name] = 'Nigeria', [Code] = 'ng' WHERE Id = 161;
UPDATE Country SET [Name] = 'Niue', [Code] = 'nu' WHERE Id = 162;
UPDATE Country SET [Name] = 'Norfolk Island', [Code] = 'nf' WHERE Id = 163;
UPDATE Country SET [Name] = 'North Macedonia', [Code] = 'mk' WHERE Id = 164;
UPDATE Country SET [Name] = 'Northern Mariana Islands', [Code] = 'mp' WHERE Id = 165;
UPDATE Country SET [Name] = 'Norway', [Code] = 'no' WHERE Id = 166;
UPDATE Country SET [Name] = 'Oman', [Code] = 'om' WHERE Id = 167;
UPDATE Country SET [Name] = 'Pakistan', [Code] = 'pk' WHERE Id = 168;
UPDATE Country SET [Name] = 'Palau', [Code] = 'pw' WHERE Id = 169;
UPDATE Country SET [Name] = 'Palestine, State of', [Code] = 'ps' WHERE Id = 170;
UPDATE Country SET [Name] = 'Panama', [Code] = 'pa' WHERE Id = 171;
UPDATE Country SET [Name] = 'Papua New Guinea', [Code] = 'pg' WHERE Id = 172;
UPDATE Country SET [Name] = 'Paraguay', [Code] = 'py' WHERE Id = 173;
UPDATE Country SET [Name] = 'Peru', [Code] = 'pe' WHERE Id = 174;
UPDATE Country SET [Name] = 'Philippines', [Code] = 'ph' WHERE Id = 175;
UPDATE Country SET [Name] = 'Pitcairn', [Code] = 'pn' WHERE Id = 176;
UPDATE Country SET [Name] = 'Poland', [Code] = 'pl' WHERE Id = 177;
UPDATE Country SET [Name] = 'Portugal', [Code] = 'pt' WHERE Id = 178;
UPDATE Country SET [Name] = 'Puerto Rico', [Code] = 'pr' WHERE Id = 179;
UPDATE Country SET [Name] = 'Qatar', [Code] = 'qa' WHERE Id = 180;
UPDATE Country SET [Name] = 'Romania', [Code] = 'ro' WHERE Id = 181;
UPDATE Country SET [Name] = 'Russian Federation', [Code] = 'ru' WHERE Id = 182;
UPDATE Country SET [Name] = 'Rwanda', [Code] = 'rw' WHERE Id = 183;
UPDATE Country SET [Name] = 'Réunion', [Code] = 're' WHERE Id = 184;
UPDATE Country SET [Name] = 'Saint Barthélemy', [Code] = 'bl' WHERE Id = 185;
UPDATE Country SET [Name] = 'Saint Helena, Ascension and Tristan da Cunha', [Code] = 'sh' WHERE Id = 186;
UPDATE Country SET [Name] = 'Saint Kitts and Nevis', [Code] = 'kn' WHERE Id = 187;
UPDATE Country SET [Name] = 'Saint Lucia', [Code] = 'lc' WHERE Id = 188;
UPDATE Country SET [Name] = 'Saint Martin (French part)', [Code] = 'mf' WHERE Id = 189;
UPDATE Country SET [Name] = 'Saint Pierre and Miquelon', [Code] = 'pm' WHERE Id = 190;
UPDATE Country SET [Name] = 'Saint Vincent and the Grenadines', [Code] = 'vc' WHERE Id = 191;
UPDATE Country SET [Name] = 'Samoa', [Code] = 'ws' WHERE Id = 192;
UPDATE Country SET [Name] = 'San Marino', [Code] = 'sm' WHERE Id = 193;
UPDATE Country SET [Name] = 'Sao Tome and Principe', [Code] = 'st' WHERE Id = 194;
UPDATE Country SET [Name] = 'Saudi Arabia', [Code] = 'sa' WHERE Id = 195;
UPDATE Country SET [Name] = 'Senegal', [Code] = 'sn' WHERE Id = 196;
UPDATE Country SET [Name] = 'Serbia', [Code] = 'rs' WHERE Id = 197;
UPDATE Country SET [Name] = 'Seychelles', [Code] = 'sc' WHERE Id = 198;
UPDATE Country SET [Name] = 'Sierra Leone', [Code] = 'sl' WHERE Id = 199;
UPDATE Country SET [Name] = 'Singapore', [Code] = 'sg' WHERE Id = 200;
UPDATE Country SET [Name] = 'Sint Maarten', [Code] = 'sx' WHERE Id = 201;
UPDATE Country SET [Name] = 'Slovakia', [Code] = 'sk' WHERE Id = 202;
UPDATE Country SET [Name] = 'Slovenia', [Code] = 'si' WHERE Id = 203;
UPDATE Country SET [Name] = 'Solomon Islands', [Code] = 'sb' WHERE Id = 204;
UPDATE Country SET [Name] = 'Somalia', [Code] = 'so' WHERE Id = 205;
UPDATE Country SET [Name] = 'South Africa', [Code] = 'za' WHERE Id = 206;
UPDATE Country SET [Name] = 'South Georgia and the South Sandwich Islands', [Code] = 'gs' WHERE Id = 207;
UPDATE Country SET [Name] = 'South Sudan', [Code] = 'ss' WHERE Id = 208;
UPDATE Country SET [Name] = 'Spain', [Code] = 'es' WHERE Id = 209;
UPDATE Country SET [Name] = 'Sri Lanka', [Code] = 'lk' WHERE Id = 210;
UPDATE Country SET [Name] = 'Sudan', [Code] = 'sd' WHERE Id = 211;
UPDATE Country SET [Name] = 'Suriname', [Code] = 'sr' WHERE Id = 212;
UPDATE Country SET [Name] = 'Svalbard and Jan Mayen', [Code] = 'sj' WHERE Id = 213;
UPDATE Country SET [Name] = 'Sweden', [Code] = 'se' WHERE Id = 214;
UPDATE Country SET [Name] = 'Switzerland', [Code] = 'ch' WHERE Id = 215;
UPDATE Country SET [Name] = 'Syrian Arab Republic', [Code] = 'sy' WHERE Id = 216;
UPDATE Country SET [Name] = 'Taiwan', [Code] = 'tw' WHERE Id = 217;
UPDATE Country SET [Name] = 'Tajikistan', [Code] = 'tj' WHERE Id = 218;
UPDATE Country SET [Name] = 'Tanzania, the United Republic of', [Code] = 'tz' WHERE Id = 219;
UPDATE Country SET [Name] = 'Thailand', [Code] = 'th' WHERE Id = 220;
UPDATE Country SET [Name] = 'Timor-Leste', [Code] = 'tl' WHERE Id = 221;
UPDATE Country SET [Name] = 'Togo', [Code] = 'tg' WHERE Id = 222;
UPDATE Country SET [Name] = 'Tokelau', [Code] = 'tk' WHERE Id = 223;
UPDATE Country SET [Name] = 'Tonga', [Code] = 'to' WHERE Id = 224;
UPDATE Country SET [Name] = 'Trinidad and Tobago', [Code] = 'tt' WHERE Id = 225;
UPDATE Country SET [Name] = 'Tunisia', [Code] = 'tn' WHERE Id = 226;
UPDATE Country SET [Name] = 'Turkmenistan', [Code] = 'tm' WHERE Id = 227;
UPDATE Country SET [Name] = 'Turks and Caicos Islands', [Code] = 'tc' WHERE Id = 228;
UPDATE Country SET [Name] = 'Tuvalu', [Code] = 'tv' WHERE Id = 229;
UPDATE Country SET [Name] = 'Türkiye', [Code] = 'tr' WHERE Id = 230;
UPDATE Country SET [Name] = 'Uganda', [Code] = 'ug' WHERE Id = 231;
UPDATE Country SET [Name] = 'Ukraine', [Code] = 'ua' WHERE Id = 232;
UPDATE Country SET [Name] = 'United Arab Emirates', [Code] = 'ae' WHERE Id = 233;
UPDATE Country SET [Name] = 'United Kingdom of Great Britain and Northern Ireland', [Code] = 'gb' WHERE Id = 234;
UPDATE Country SET [Name] = 'United States Minor Outlying Islands', [Code] = 'um' WHERE Id = 235;
UPDATE Country SET [Name] = 'United States of America', [Code] = 'us' WHERE Id = 236;
UPDATE Country SET [Name] = 'Uruguay', [Code] = 'uy' WHERE Id = 237;
UPDATE Country SET [Name] = 'Uzbekistan', [Code] = 'uz' WHERE Id = 238;
UPDATE Country SET [Name] = 'Vanuatu', [Code] = 'vu' WHERE Id = 239;
UPDATE Country SET [Name] = 'Venezuela', [Code] = 've' WHERE Id = 240;
UPDATE Country SET [Name] = 'Viet Nam', [Code] = 'vn' WHERE Id = 241;
UPDATE Country SET [Name] = 'Virgin Islands (British)', [Code] = 'vg' WHERE Id = 242;
UPDATE Country SET [Name] = 'Virgin Islands (U.S.)', [Code] = 'vi' WHERE Id = 243;
UPDATE Country SET [Name] = 'Wallis and Futuna', [Code] = 'wf' WHERE Id = 244;
UPDATE Country SET [Name] = 'Western Sahara*', [Code] = 'eh' WHERE Id = 245;
UPDATE Country SET [Name] = 'Yemen', [Code] = 'ye' WHERE Id = 246;
UPDATE Country SET [Name] = 'Zambia', [Code] = 'zm' WHERE Id = 247;
UPDATE Country SET [Name] = 'Zimbabwe', [Code] = 'zw' WHERE Id = 248;
UPDATE Country SET [Name] = 'Åland Islands', [Code] = 'ax' WHERE Id = 249;

INSERT INTO [Country] ([Name],[Code]) VALUES ('England', 'gb-eng');
INSERT INTO [Country] ([Name],[Code]) VALUES ('Northern Ireland', 'gb-nir');
INSERT INTO [Country] ([Name],[Code]) VALUES ('Scotland', 'gb-sct');
INSERT INTO [Country] ([Name],[Code]) VALUES ('United Nations', 'un');
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("Code", "Country");
        }
    }
}