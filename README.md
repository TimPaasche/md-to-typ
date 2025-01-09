# Introduction

This repository contains a small tool that converts Markdown (.md) files to Typst documents. Typst is a modern typesetting system that allows for the creation of high-quality PDF documents. This tool facilitates the conversion process, enabling users to easily transform their Markdown content into Typst format, which can then be exported as a PDF.

- [Dependencies](#dependencies)
- [Installation](#installation)
  - [Prebuild](#prebuild-floppy_disk)
  - [Build it yourself](#build-it-yourself-hammer)
- [Usage](#usage)
- [What is still missing?](#what-is-still-missing)
- [License](#license)

# Dependencies

- [Typst](https://github.com/typst/typst)
- [node.js](https://nodejs.org/en)
- [tex-to-txpst](https://github.com/continuous-foundation/tex-to-typst?tab=readme-ov-file#readme)

# Installation

## Prebuild :floppy_disk:

COMING SOON! :smile:

## Build it yourself :hammer:

1. Clone the repository to your local machine.
2. Install Typst ([Install-Guide](https://github.com/typst/typst?tab=readme-ov-file#installation))
3. Install Node.js ([Instatll Link](https://nodejs.org/en/download))
4. Change the copy direktive of `convert.js`, `package.json`, `package-lock.json` and `node_modules` in the MdToTypst.csproj to your local paths 
5. Build the project with 
```
dotnet build --configuration Release
```
4. (optinal) PATH the `md-export` executable
5. enjoy converting!

# Usage

To convert a Markdown file to Typst, run the following command:

```
md-export <input> <flags>
```

|         Flag         | Description                                                                                                                                               |
|:--------------------:|:----------------------------------------------------------------------------------------------------------------------------------------------------------|
|  `-o` or `--output`  | Specifies the output directory. If not provided, the output file will be the same as the input file, but with a `.typ` or `.pdf` extension.               |
|  `-t` or `--title`   | Specifies the file name of the export.                                                                                                                    |
|  `-s` or `--style`   | Specifies the styling template, that should be used for the export.                                                                                       |
|   `-p` or `--pdf`    | Specifies the output file format. If not provided, the output file will be a `.typ` file. If the flag is provided it will be compiled with typst to pdf.  |

# What is still missing?

- Unit Tests
- full emoji support
- better bockquotes

# License

MIT License
