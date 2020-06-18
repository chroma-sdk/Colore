#!/bin/sh

# Define default arguments.
SCRIPT="build.cake"
TARGET="Default"
VERBOSITY="Normal"
CAKE_ARGUMENTS=""

# Parse arguments.
for i in "$@"; do
    case $1 in
        -s|--script) SCRIPT="$2"; shift $(( $# > 0 ? 1 : 0 )) ;;
        -t|--target) TARGET="$2"; shift $(( $# > 0 ? 1 : 0 )) ;;
        -V|--verbosity) VERBOSITY="$2"; shift $(( $# > 0 ? 1 : 0 )) ;;
        --) shift $(( $# > 0 ? 1 : 0 )); CAKE_ARGUMENTS="${CAKE_ARGUMENTS} $@"; break ;;
        *) CAKE_ARGUMENTS="${CAKE_ARGUMENTS} $1" ;;
    esac
    shift $(( $# > 0 ? 1 : 0 ))
done
set -- ${CAKE_ARGUMENTS}

# Restore Cake tool
dotnet tool restore

if [ $? -ne 0 ]; then
    echo "An error occured while installing Cake."
    exit 1
fi

# Start Cake
dotnet tool run dotnet-cake --bootstrap --verbosity=$VERBOSITY
dotnet tool run dotnet-cake "--" "$SCRIPT" --target="$TARGET" "$@" --verbosity=$VERBOSITY
