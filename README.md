# wudecon
A multi-purpose tool built with [ShenmueDKSharp](https://github.com/philyeahz/ShenmueDKSharp) to convert/unpack a variety of file formats used in Shenmue I and Shenmue II.

```
wudecon v1.0.7023.39908

        wudecon <mode> <input> <output>
        wudecon --all <input dir> <output dir>
        wudecon --mt5 <mt5 file> <obj file>
        wudecon --mt5 <dir with mt5's> <output dir>
        wudecon --mt7 <mt7 file> <obj file>
        wudecon [--pkf|--pks|--spr|--ipac|--gz|--afs] <source file> <output dir>
        wudecon --tc <tad file> <output dir>
        wudecon --tacfile <file in tac to extract> <output dir>
        wudecon --tacfull <tad file> <tac output dir> <model output dir> <mt5/mt7>

        Batch conversion possible by replacing file argument for path
        When using --tacfile (or -tfile) the path to search for within the TAC needs to be in lowercase.
        For verbose logging, add 'v' to the beginning or end of the mode, e.g. '--allv' or 'v--mt5'
```
