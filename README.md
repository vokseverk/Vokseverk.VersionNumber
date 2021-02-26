# Umbraco Property Editor Template

This is our starter project for an Umbraco Property Editor.

## Process

- After creating a new project from this template, rename the root folder from the
default "package-template" to something that suits your project

- Then find the `src/package.ent` file and change the constants in there

- You may want to change a few things in the `package.xml` and `manifest.xml` files as well

- Look for `TODO`, `EDITOR_NAME` and `EDITOR_ALIAS` in various files and change accordingly

- Edit/add/remove files in the `src/` folder and update the files section in the
`package.xml` file

- Edit the `PropertyConverterV(7|8).cs` files as well

- Edit this README to describe your new property editor :)

- Create an entry for your new package on [Our Umbraco][OURPKG]


## Developing & Building

On macOS you can run the `build.sh` script from the terminal, which will
build a ZIP file in the `dist` folder that is installable from
Umbraco 8's _Packages_ section or Umbraco 7's _Developer > Packages_ section.

The build script versions the files so it's easier to test the package inside
an Umbraco installation by uninstalling the existing version and then
installing a new build. Existing data-types keep their data as long as their
alias and/or storage type isn't changed.

To update the version number, increment the `packageVersion` entity in the
`src/package.ent` file.

[OURPKG]: https://our.umbraco.com/member/profile/packages/
 
