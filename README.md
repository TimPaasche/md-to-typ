# Introduction

This repository contains a small tool that converts Markdown (.md) files to Typst documents. Typst is a modern typesetting system that allows for the creation of high-quality PDF documents. This tool facilitates the conversion process, enabling users to easily transform their Markdown content into Typst format, which can then be exported as a PDF.

# What is still missing?

- Conversion of the LaTeX code to Typst

# Installation

1. Clone the repository to your local machine.
2. Install Typst ([Install-Guide](https://github.com/typst/typst?tab=readme-ov-file#installation))
3. Build the project with 
```
dotnet build --configuration Release
```
4. PATH the `md-export` executable 

# Usage

To convert a Markdown file to Typst, run the following command:

```ps
md-export <input-file> <flags>
```

|         Flag         | Description                                                                                                                                               |
|:--------------------:|:----------------------------------------------------------------------------------------------------------------------------------------------------------|
|  `-o` or `--output`  | Specifies the output directory. If not provided, the output file will be the same as the input file, but with a `.typ` or `.pdf` extension.               |
|  `-t` or `--title`   | Specifies the file name of the export.                                                                                                                    |
|  `-s` or `--style`   | Specifies the styling template, that should be used for the export.                                                                                       |
|   `-p` or `--pdf`    | Specifies the output file format. If not provided, the output file will be a `.typ` file. If the flag is provided it will be compiled with typst to pdf.  |

# License

MIT License